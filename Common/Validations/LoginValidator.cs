﻿using Common.DTOs.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validations
{
    public class LoginValidator : AbstractValidator<UserDto>
    {
    }
}
