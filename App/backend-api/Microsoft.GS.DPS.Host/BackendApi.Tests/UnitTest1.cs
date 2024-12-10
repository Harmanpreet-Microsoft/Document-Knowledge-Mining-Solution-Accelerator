using System;
using FluentValidation.TestHelper;
using Xunit;

namespace Microsoft.GS.DPS.Model.UserInterface.Tests
{
    public class PagingRequestValidatorTests
    {
        private readonly PagingRequestValidator _validator;

        public PagingRequestValidatorTests()
        {
            _validator = new PagingRequestValidator();
        }

        [Fact]
        public void Validate_ValidPagingRequest_ShouldPassValidation()
        {
            var request = new PagingRequest
            {
                PageNumber = 1,
                PageSize = 10,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow
            };

            var result = _validator.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_PageNumberIsZero_ShouldFailValidation()
        {
            var request = new PagingRequest
            {
                PageNumber = 0,
                PageSize = 10
            };

            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.PageNumber);
        }

        [Fact]
        public void Validate_PageSizeIsNegative_ShouldFailValidation()
        {
            var request = new PagingRequest
            {
                PageNumber = 1,
                PageSize = -5
            };

            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.PageSize);
        }

        [Fact]
        public void Validate_OptionalFieldsNull_ShouldPassValidation()
        {
            var request = new PagingRequest
            {
                PageNumber = 1,
                PageSize = 10,
                StartDate = null,
                EndDate = null
            };

            var result = _validator.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
