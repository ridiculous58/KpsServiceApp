﻿using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
