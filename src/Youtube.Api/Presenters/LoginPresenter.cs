﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Youtube.Api.Core.Dto.UseCaseResponses;
using Youtube.Api.Core.Interfaces;
using Youtube.Api.Serialization;

namespace Youtube.Api.Presenters
{
    public class LoginPresenter : IOutputPort<LoginResponse>
    {
        public JsonContentResult ContentResult { get; }

        public LoginPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(LoginResponse response)
        {
            ContentResult.StatusCode = response.Success ? 200 : response.Errors.First().Code;
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
