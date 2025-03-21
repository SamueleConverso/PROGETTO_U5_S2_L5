﻿using Microsoft.AspNetCore.Identity;

namespace PROGETTO_U5_S2_L5.Models {
    public class ApplicationRole : IdentityRole {
        public ICollection<ApplicationUserRole> ApplicationUserRole {
            get; set;
        }
    }
}
