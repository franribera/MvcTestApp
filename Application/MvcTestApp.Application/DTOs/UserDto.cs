﻿using System;
using System.Collections.Generic;

namespace MvcTestApp.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
