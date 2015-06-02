namespace WorkshopExpert.Infrastructure.Migrations
{
    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WorkshopExpert.Core.Entities;
using WorkshopExpert.Infrastructure.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkshopExpert.Infrastructure.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            
            SeedCountries(context);
            SeedBusinessTypes(context);
            //dependent of country and Business types so order is important
            SeedAdminUsers(context);
            SeedCategories(context);
            SeedWorkshopTypes(context);
            SeedDeliveryMethods(context);
            SeedTimeSlots(context);
            context.SaveChanges();
        }

        private void SeedTimeSlots(ApplicationDbContext context)
        {
            var timeSlots = new List<TimeSlot>
            {
                new TimeSlot { Name = "8:00 AM"},
                new TimeSlot { Name = "10:00 AM"},
                new TimeSlot { Name = "12:00 PM"},
                new TimeSlot { Name = "2:30 PM"},
            };

            foreach (var timeSlot in timeSlots)
            {
                if (!context.TimeSlots.Any(t => t.Name == timeSlot.Name))
                {
                    context.TimeSlots.Add(timeSlot);
                }
            }            
        }



        /// <summary>
        /// Seed admin user
        /// </summary>
        /// <param name="context"></param>
        private void SeedAdminUsers(ApplicationDbContext context)
        {
            string[] users =  { "expert@gmail.com", "learner@gmail.com", "admin@gmail.com" };
            string[] userRoles =  { "expert", "learner", "admin" };

            //add roles
            for(var i = 0; i < 3; i++)
            {
                var userRole = userRoles[i]; // need this since we can't pass an array index to LINQ expression
                if (!context.Roles.Any(r => r.Name == userRole))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var identityRole = new IdentityRole { Name = userRole };

                    manager.Create(identityRole);
                }
            }

            //add users w/ assigned roles
            for (int i = 0; i < 3; i++)
			{
                var user = users[i];
                var userRole = userRoles[i];

                //seed method for countries must come first
                var countryId = context.Countries.FirstOrDefault().Id;
                var businessTypeId = context.BusinessTypes.FirstOrDefault().Id;

                if (!context.Users.Any(u => u.UserName == user))
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var manager = new UserManager<ApplicationUser>(store);
                    var identityUser = new ApplicationUser { 
                        UserName = user, 
                        Country_Id = countryId, 
                        BusinessType_Id = businessTypeId 
                    };

                    manager.Create(identityUser, "abc123");
                    manager.AddToRole(identityUser.Id, userRole);
                }
			}
        }

        private void SeedWorkshopTypes(ApplicationDbContext context)
        {
            var workshopList = new List<WorkshopType>
            {
                new WorkshopType { Id = 1, Name = "In-House Workshop"},
                new WorkshopType { Id = 2, Name = "Open Workshop"},
                new WorkshopType { Id = 3, Name = "Virtual Workshop"}
            };

            foreach (var ws in workshopList)
            {
                if (!context.WorkshopTypes.Any(w => w.Id == ws.Id))
                {
                    context.WorkshopTypes.Add(ws);
                }
            }
            
        }


        private void SeedCategories(ApplicationDbContext context)
        {
            var catgyList = new List<Category>
            {
                new Category { Id = new Guid("4368afd1-790e-481a-9b5e-2404e75ae68b"), Name = "Technology And Innovation"},
                new Category { Id = new Guid("d5ef450a-a11b-48a7-b81f-69d221a65324"), Name = "Software Development"},
                new Category { Id = new Guid("46d4eb95-87c5-4767-8bd4-da2c9030bf58"), Name = "Business Communication"}
            };

            foreach (var catgy in catgyList)
            {
                if (!context.Categories.Any(c => c.Id == c.Id))
                {
                    context.Categories.Add(catgy);
                }
            }
        }

        private void SeedDeliveryMethods(ApplicationDbContext context)
        {
            var deliveryMethods = new List<DeliveryMethod>
            {
                new DeliveryMethod { Id = 1 , Name = "Case Study", ShortName = "C" },
                new DeliveryMethod { Id = 2 , Name = "Lecture", ShortName = "L" },
                new DeliveryMethod { Id = 3 , Name = "Debate", ShortName = "D" },
                new DeliveryMethod { Id = 4 , Name = "Table Exercise", ShortName ="TE" },
                new DeliveryMethod { Id = 5 , Name = "Class Exercise", ShortName ="CE" },
                new DeliveryMethod { Id = 6 , Name = "Game", ShortName ="G" },
                new DeliveryMethod { Id = 7 , Name = "Action Planning", ShortName ="AP"},
            };

            foreach (var catgy in deliveryMethods)
            {
                if (!context.DeliveryMethods.Any(d => d.Name == d.Name))
                {
                    context.DeliveryMethods.Add(catgy);
                }
            }
        }

        private void SeedBusinessTypes(ApplicationDbContext context)
        {
            var businessTypes = new List<BusinessType>
            {
                new BusinessType { Name = "Information Technology"},
                new BusinessType { Name = "Manufacturing" },
                new BusinessType { Name = "Marketing" },
                new BusinessType { Name = "Finance" },

            };

            foreach (var busType in businessTypes)
            {
                if (!context.BusinessTypes.Any(c => c.Name == busType.Name))
                {
                    context.BusinessTypes.Add(busType);
                }
            }
        }

        public void SeedCountries(ApplicationDbContext context)
        {
            var countries = new List<Country>
            {
                new Country { Id =1 , Name ="Afghanistan"},
                new Country { Id =2 , Name ="Albania"},
                new Country { Id =3 , Name ="Algeria"},
                new Country { Id =4 , Name ="Andorra"},
                new Country { Id =5 , Name ="Angola"},
                new Country { Id =6 , Name ="Antigua and Barbuda"},
                new Country { Id =7 , Name ="Argentina"},
                new Country { Id =8 , Name ="Armenia"},
                new Country { Id =9 , Name ="Australia"},
                new Country { Id =10 , Name ="Austria"},
                new Country { Id =11 , Name ="Azerbaijan"},
                new Country { Id =12 , Name ="Bahamas"},
                new Country { Id =13 , Name ="Bahrain"},
                new Country { Id =14 , Name ="Bangladesh"},
                new Country { Id =15 , Name ="Barbados"},
                new Country { Id =16 , Name ="Belarus"},
                new Country { Id =17 , Name ="Belgium"},
                new Country { Id =18 , Name ="Belize"},
                new Country { Id =19 , Name ="Benin"},
                new Country { Id =20 , Name ="Bhutan"},
                new Country { Id =21 , Name ="Bolivia"},
                new Country { Id =22 , Name ="Bosnia and Herzegovina"},
                new Country { Id =23 , Name ="Botswana"},
                new Country { Id =24 , Name ="Brazil"},
                new Country { Id =25 , Name ="Brunei Darussalam"},
                new Country { Id =26 , Name ="Bulgaria"},
                new Country { Id =27 , Name ="Burkina Faso"},
                new Country { Id =28 , Name ="Burundi"},
                new Country { Id =29 , Name ="Cabo Verde"},
                new Country { Id =30 , Name ="Cambodia"},
                new Country { Id =31 , Name ="Cameroon"},
                new Country { Id =32 , Name ="Canada"},
                new Country { Id =33 , Name ="Central African Republic"},
                new Country { Id =34 , Name ="Chad"},
                new Country { Id =35 , Name ="Chile"},
                new Country { Id =36 , Name ="China"},
                new Country { Id =37 , Name ="Colombia"},
                new Country { Id =38 , Name ="Comoros"},
                new Country { Id =39 , Name ="Congo"},
                new Country { Id =40 , Name ="Costa Rica"},
                new Country { Id =41 , Name ="Côte d'Ivoire"},
                new Country { Id =42 , Name ="Croatia"},
                new Country { Id =43 , Name ="Cuba"},
                new Country { Id =44 , Name ="Cyprus"},
                new Country { Id =45 , Name ="Czech Republic"},
                new Country { Id =46 , Name ="Democratic People's Republic of Korea (North Korea)"},
                new Country { Id =47 , Name ="Democratic Republic of the Cong"},
                new Country { Id =48 , Name ="Denmark"},
                new Country { Id =49 , Name ="Djibouti"},
                new Country { Id =50 , Name ="Dominica"},
                new Country { Id =51 , Name ="Dominican Republic"},
                new Country { Id =52 , Name ="Ecuador"},
                new Country { Id =53 , Name ="Egypt"},
                new Country { Id =54 , Name ="El Salvador"},
                new Country { Id =55 , Name ="Equatorial Guinea"},
                new Country { Id =56 , Name ="Eritrea"},
                new Country { Id =57 , Name ="Estonia"},
                new Country { Id =58 , Name ="Ethiopia"},
                new Country { Id =59 , Name ="Fiji"},
                new Country { Id =60 , Name ="Finland"},
                new Country { Id =61 , Name ="France"},
                new Country { Id =62 , Name ="Gabon"},
                new Country { Id =63 , Name ="Gambia"},
                new Country { Id =64 , Name ="Georgia"},
                new Country { Id =65 , Name ="Germany"},
                new Country { Id =66 , Name ="Ghana"},
                new Country { Id =67 , Name ="Greece"},
                new Country { Id =68 , Name ="Grenada"},
                new Country { Id =69 , Name ="Guatemala"},
                new Country { Id =70 , Name ="Guinea"},
                new Country { Id =71 , Name ="Guinea-Bissau"},
                new Country { Id =72 , Name ="Guyana"},
                new Country { Id =73 , Name ="Haiti"},
                new Country { Id =74 , Name ="Honduras"},
                new Country { Id =75 , Name ="Hungary"},
                new Country { Id =76 , Name ="Iceland"},
                new Country { Id =77 , Name ="India"},
                new Country { Id =78 , Name ="Indonesia"},
                new Country { Id =79 , Name ="Iran"},
                new Country { Id =80 , Name ="Iraq"},
                new Country { Id =81 , Name ="Ireland"},
                new Country { Id =82 , Name ="Israel"},
                new Country { Id =83 , Name ="Italy"},
                new Country { Id =84 , Name ="Jamaica"},
                new Country { Id =85 , Name ="Japan"},
                new Country { Id =86 , Name ="Jordan"},
                new Country { Id =87 , Name ="Kazakhstan"},
                new Country { Id =88 , Name ="Kenya"},
                new Country { Id =89 , Name ="Kiribati"},
                new Country { Id =90 , Name ="Kuwait"},
                new Country { Id =91 , Name ="Kyrgyzstan"},
                new Country { Id =92 , Name ="Lao People's Democratic Republic (Laos)"},
                new Country { Id =93 , Name ="Latvia"},
                new Country { Id =94 , Name ="Lebanon"},
                new Country { Id =95 , Name ="Lesotho"},
                new Country { Id =96 , Name ="Liberia"},
                new Country { Id =97 , Name ="Libya"},
                new Country { Id =98 , Name ="Liechtenstein"},
                new Country { Id =99 , Name ="Lithuania"},
                new Country { Id =100 , Name ="Luxembourg"},
                new Country { Id =101 , Name ="Macedonia"},
                new Country { Id =102 , Name ="Madagascar"},
                new Country { Id =103 , Name ="Malawi"},
                new Country { Id =104 , Name ="Malaysia"},
                new Country { Id =105 , Name ="Maldives"},
                new Country { Id =106 , Name ="Mali"},
                new Country { Id =107 , Name ="Malta"},
                new Country { Id =108 , Name ="Marshall Islands"},
                new Country { Id =109 , Name ="Mauritania"},
                new Country { Id =110 , Name ="Mauritius"},
                new Country { Id =111 , Name ="Mexico"},
                new Country { Id =112 , Name ="Micronesia (Federated States of)"},
                new Country { Id =113 , Name ="Monaco"},
                new Country { Id =114 , Name ="Mongolia"},
                new Country { Id =115 , Name ="Montenegro"},
                new Country { Id =116 , Name ="Morocco"},
                new Country { Id =117 , Name ="Mozambique"},
                new Country { Id =118 , Name ="Myanmar"},
                new Country { Id =119 , Name ="Namibia"},
                new Country { Id =120 , Name ="Nauru"},
                new Country { Id =121 , Name ="Nepal"},
                new Country { Id =122 , Name ="Netherlands"},
                new Country { Id =123 , Name ="New Zealand"},
                new Country { Id =124 , Name ="Nicaragua"},
                new Country { Id =125 , Name ="Niger"},
                new Country { Id =126 , Name ="Nigeria"},
                new Country { Id =127 , Name ="Norway"},
                new Country { Id =128 , Name ="Oman"},
                new Country { Id =129 , Name ="Pakistan"},
                new Country { Id =130 , Name ="Palau"},
                new Country { Id =131 , Name ="Panama"},
                new Country { Id =132 , Name ="Papua New Guinea"},
                new Country { Id =133 , Name ="Paraguay"},
                new Country { Id =134 , Name ="Peru"},
                new Country { Id =135 , Name ="Philippines"},
                new Country { Id =136 , Name ="Poland"},
                new Country { Id =137 , Name ="Portugal"},
                new Country { Id =138 , Name ="Qatar"},
                new Country { Id =139 , Name ="Republic of Korea (South Korea)"},
                new Country { Id =140 , Name ="Republic of Moldova"},
                new Country { Id =141 , Name ="Romania"},
                new Country { Id =142 , Name ="Russian Federation"},
                new Country { Id =143 , Name ="Rwanda"},
                new Country { Id =144 , Name ="Saint Kitts and Nevis"},
                new Country { Id =145 , Name ="Saint Lucia"},
                new Country { Id =146 , Name ="Saint Vincent and the Grenadines"},
                new Country { Id =147 , Name ="Samoa"},
                new Country { Id =148 , Name ="San Marino"},
                new Country { Id =149 , Name ="Sao Tome and Principe"},
                new Country { Id =150 , Name ="Saudi Arabia"},
                new Country { Id =151 , Name ="Senegal"},
                new Country { Id =152 , Name ="Serbia"},
                new Country { Id =153 , Name ="Seychelles"},
                new Country { Id =154 , Name ="Sierra Leone"},
                new Country { Id =155 , Name ="Singapore"},
                new Country { Id =156 , Name ="Slovakia"},
                new Country { Id =157 , Name ="Slovenia"},
                new Country { Id =158 , Name ="Solomon Islands"},
                new Country { Id =159 , Name ="Somalia"},
                new Country { Id =160 , Name ="South Africa"},
                new Country { Id =161 , Name ="South Sudan"},
                new Country { Id =162 , Name ="Spain"},
                new Country { Id =163 , Name ="Sri Lanka"},
                new Country { Id =164 , Name ="Sudan"},
                new Country { Id =165 , Name ="Suriname"},
                new Country { Id =166 , Name ="Swaziland"},
                new Country { Id =167 , Name ="Sweden"},
                new Country { Id =168 , Name ="Switzerland"},
                new Country { Id =169 , Name ="Syrian Arab Republic"},
                new Country { Id =170 , Name ="Tajikistan"},
                new Country { Id =171 , Name ="Thailand"},
                new Country { Id =172 , Name ="Timor-Leste"},
                new Country { Id =173 , Name ="Togo"},
                new Country { Id =174 , Name ="Tonga"},
                new Country { Id =175 , Name ="Trinidad and Tobago"},
                new Country { Id =176 , Name ="Tunisia"},
                new Country { Id =177 , Name ="Turkey"},
                new Country { Id =178 , Name ="Turkmenistan"},
                new Country { Id =179 , Name ="Tuvalu"},
                new Country { Id =180 , Name ="Uganda"},
                new Country { Id =181 , Name ="Ukraine"},
                new Country { Id =182 , Name ="United Arab Emirates"},
                new Country { Id =183 , Name ="United Kingdom of Great Britain and Northern Ireland"},
                new Country { Id =184 , Name ="United Republic of Tanzania"},
                new Country { Id =185 , Name ="United States of America"},
                new Country { Id =186 , Name ="Uruguay"},
                new Country { Id =187 , Name ="Uzbekistan"},
                new Country { Id =188 , Name ="Vanuatu"},
                new Country { Id =189 , Name ="Venezuela"},
                new Country { Id =190 , Name ="Vietnam"},
                new Country { Id =191 , Name ="Yemen"},
                new Country { Id =192 , Name ="Zambia"},
                new Country { Id =193 , Name ="Zimbabwe"},
            };

            foreach (var country in countries)
            {
                if (!context.Countries.Any( c => c.Name == country.Name) )
                {
                    context.Countries.Add(country);
                }
            }

        }
    }
}
