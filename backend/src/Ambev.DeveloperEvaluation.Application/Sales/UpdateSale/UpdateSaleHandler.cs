﻿using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {

        private readonly ISaleRepository _saleRepository;

        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var updateSale = _mapper.Map<Sale>(request);


            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            sale.Update(updateSale);

            var saleValidator = new SaleValidator();
            var saleValidationResult = await saleValidator.ValidateAsync(sale, cancellationToken);

            if (!saleValidationResult.IsValid)
                throw new ValidationException(saleValidationResult.Errors);


            var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

            updatedSale.Items.RemoveAll(i => i.IsCanceled);

            return _mapper.Map<UpdateSaleResult>(updatedSale);

        }
    }
}
