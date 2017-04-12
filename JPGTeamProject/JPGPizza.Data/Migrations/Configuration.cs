namespace JPGPizza.Data.Migrations
{
    using JPGPizza.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JPGPizzaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JPGPizzaDbContext context)
        {
            // Seed Users
            if (!context.Users.Any())
            {
                CreateUser(context, "yanislav.asenov@gmail.com", "123", "Yanislav", "Asenov", 5, Gender.Male);
                CreateUser(context, "petio.vasilev@gmail.com", "123", "Petio", "Vasilev", 6, Gender.Male);
                CreateUser(context, "georgi.georgiev@gmail.com", "123", "Georgi", "Georgiev", 7, Gender.Male);
            }

            // Seed Roles
            if (!context.Roles.Any())
            {
                CreateRole(context, "Administrator");
                CreateRole(context, "User");

                // Seed Roles To Users
                AddUserToRole(context, "yanislav.asenov@gmail.com", "Administrator");
                AddUserToRole(context, "petio.vasilev@gmail.com", "Administrator");
                AddUserToRole(context, "georgi.georgiev@gmail.com", "Administrator");
            }

            // Seed Products
            if (!context.Products.Any())
            {
                CreateProduct(context, "Маргарита", "", 9.50m, "http://westport.tarrylodge.com/wp-content/uploads/sites/17/2015/08/Babbo_Boston_Food_703-740x380.jpg");
                CreateProduct(context, "Пеперони", "", 13m, "http://balkan-pizza-doner.de/wp-content/uploads/2015/08/Pepperoni_1.jpg");
                CreateProduct(context, "Гардън", "", 11m, "https://cdn.media.yp.ca/8621438/pcc-5649004053842719950-IMG-0072_r.JPG");
                CreateProduct(context, "Американa", "", 12m, "http://www.silviocicchi.com/pizzachef/wp-content/uploads/2015/02/a-evid-672x372.jpg");
                CreateProduct(context, "Meat Mania", "", 14.50m, "http://68.media.tumblr.com/3413204e7b24a9a690085326cab23e41/tumblr_o6ijpa8Vjx1tg05wco1_1280.jpg");
                CreateProduct(context, "Хавай", "", 11m, "https://s3.amazonaws.com/ODNUploads/53c7197f0d5d1hawaiian.jpg");
            }

            // Seed Ingredients
            if (!context.Ingredinets.Any())
            {
                CreateIngredient(context, "Доматен сос", "");
                CreateIngredient(context, "Моцарела", "");
                CreateIngredient(context, "Пушена шунка", "");
                CreateIngredient(context, "Ананас", "");
                CreateIngredient(context, "Пеперони", "");
                CreateIngredient(context, "Люти чушки", "");
                CreateIngredient(context, "Лук", "");
                CreateIngredient(context, "Зелени чушки", "");
                CreateIngredient(context, "Домати", "");
                CreateIngredient(context, "Черни маслини", "");
                CreateIngredient(context, "Гъби", "");
                CreateIngredient(context, "Пушен бекон", "");
                CreateIngredient(context, "Пиле", "");
                CreateIngredient(context, "Пушена шунка", "");
                CreateIngredient(context, "Чоризо", "");
                CreateIngredient(context, "Пикантно телешко", "");

                // Seed Ingredients To Products
                AddIngretientsToProduct(context, "Маргарита", "Доматен сос", "Моцарела");
                AddIngretientsToProduct(context, "Пеперони", "Доматен сос", "Моцарела", "Пеперони");
                AddIngretientsToProduct(context, "Гардън", "Доматен сос", "Моцарела", "Лук", "Зелени чушки", "Домати", "Черни маслини", "Гъби");
                AddIngretientsToProduct(context, "Американa", "Доматен сос", "Моцарела", "Пеперони", "Люти чушки", "Лук");
                AddIngretientsToProduct(context, "Meat Mania", "Доматен сос", "Моцарела", "Чоризо", "Пикантно телешко", "Пушен бекон", "Пиле", "Пушена шунка");
                AddIngretientsToProduct(context, "Хавай", "Доматен сос", "Моцарела", "Пушена шунка", "Ананас");
            }

            // Insert Feedback For Product By User
            if (!context.Feedbacks.Any())
            {
                CreateFeedbackForProduct(context, "Маргарита", "yanislav.asenov@gmail.com", "Беше много вкусна и нямам търпение да си взема пак.");
                CreateFeedbackForProduct(context, "Маргарита", "petio.vasilev@gmail.com", "Не мислех, че ще ми хареса, но ми хареса #k.");
                CreateFeedbackForProduct(context, "Пеперони", "petio.vasilev@gmail.com", "Баси яката пица. Поръчайте си поне две!");
                CreateFeedbackForProduct(context, "Пеперони", "yanislav.asenov@gmail.com", "Пръснах се от ядене. Мега вкусната пица!");
                CreateFeedbackForProduct(context, "Пеперони", "georgi.georgiev@gmail.com", "Много ми хареса! Няма да сгрешите, ако си я поръчате.");
                CreateFeedbackForProduct(context, "Гардън", "georgi.georgiev@gmail.com", "Доста вкусна. Поръчвайте!");
                CreateFeedbackForProduct(context, "Американa", "yanislav.asenov@gmail.com", "Ако не броим лютите чушки, които не лютяха беше доста добра.");
                CreateFeedbackForProduct(context, "Meat Mania", "petio.vasilev@gmail.com", "Месото беше уникално, но доматения сос беше малко.");
                CreateFeedbackForProduct(context, "Хавай", "yanislav.asenov@gmail.com", "Очаквах да е по-вкусна отколото беше...");
            }

            // Insert Orders For Users 
            // With Random Chosen Products And Quantity
            if (!context.Orders.Any())
            {
                for (var i = 0; i < 6; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "yanislav.asenov@gmail.com", (int)Math.Ceiling(1 + i / 2.0), 1, 1 + i);
                }

                for (var i = 0; i < 3; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "petio.vasilev@gmail.com", 3, 1, 1 + i);
                }

                for (var i = 0; i < 15; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "georgi.georgiev@gmail.com", 1 + i, 1, 1 + i);
                }
            }
        }

        private void InsertOrderWithRandomOrderProductsForUser(JPGPizzaDbContext context, string username, int numberOfOrderProducts, int minQuantity, int maxQuantity)
        {
            var targetUser = context.Users.FirstOrDefault(u => u.UserName == username);

            if (targetUser == null)
            {
                return;
            }

            var order = new Order()
            {
                Date = DateTime.Now
            };

            var rnd = new Random();
            var products = context.Products.ToList();

            if (numberOfOrderProducts >= products.Count)
            {
                numberOfOrderProducts = products.Count - 1;
            }

            for (var i = 0; i < numberOfOrderProducts; i++)
            {
                var productIndex = rnd.Next(0, products.Count - 1);
                var product = products[productIndex];

                order.OrderProducts.Add(new OrderProduct()
                {
                    Product = product,
                    ProductId = product.Id,
                    Quantity = rnd.Next(minQuantity, maxQuantity)
                });

                products.RemoveAt(productIndex);
            }

            targetUser.Orders.Add(order);
            context.SaveChanges();
        }

        private void CreateFeedbackForProduct(JPGPizzaDbContext context, string productName, string username, string feedbackContent)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return;
            }

            var targetProduct = context.Products.FirstOrDefault(p => p.Name == productName);

            if (targetProduct == null)
            {
                return;
            }

            var rnd = new Random();

            var feedback = new Feedback()
            {
                Content = feedbackContent,
                Customer = user,
                CustomerId = user.Id,
                ProductId = targetProduct.Id,
                CreatedOn = DateTime.Now.AddDays(rnd.Next(0, 30))
            };

            targetProduct.Feedbacks.Add(feedback);
            context.SaveChanges();
        }

        private void AddIngretientsToProduct(JPGPizzaDbContext context, string productName, params string[] ingredientNames)
        {
            var targetProduct = context.Products.FirstOrDefault(p => p.Name == productName);

            if (targetProduct == null)
            {
                return;
            }

            var ingredients = context.Ingredinets.Where(i => ingredientNames.Contains(i.Name)).ToList();

            foreach (var ingredient in ingredients)
            {
                targetProduct.Ingredients.Add(ingredient);
            }

            context.SaveChanges();
        }

        private void CreateIngredient(JPGPizzaDbContext context, string name, string description)
        {
            var ingredient = new Ingredient()
            {
                Name = name,
                Description = description
            };

            context.Ingredinets.Add(ingredient);
            context.SaveChanges();
        }

        private void CreateProduct(JPGPizzaDbContext context, string name, string description, decimal price, string picture)
        {
            var product = new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                Picture = picture
            };

            context.Products.Add(product);
            context.SaveChanges();
        }

        private void CreateUser(JPGPizzaDbContext context, string email, string password, string firstName, string lastName, int age, Gender gender)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var rnd = new Random();

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender,
                RegisteredOn = DateTime.Now.AddDays(rnd.Next(0, 300))
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(JPGPizzaDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(JPGPizzaDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

    }
}
