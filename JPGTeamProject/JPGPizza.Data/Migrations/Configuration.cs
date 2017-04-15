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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JPGPizzaDbContext context)
        {
            // Seed Users
            if (!context.Users.Any())
            {
                CreateUser(context, "yani", "yanislav.asenov@gmail.com", "123", "Yanislav", "Asenov", 5, Gender.Male);
                CreateUser(context, "petio", "petio.vasilev@gmail.com", "123", "Petio", "Vasilev", 6, Gender.Male);
                CreateUser(context, "joro", "georgi.georgiev@gmail.com", "123", "Georgi", "Georgiev", 7, Gender.Male);
                CreateUser(context, "pesho", "pesho.pesho@gmail.com", "123", "Pesho", "Pesho", 8, Gender.Male);
            }

            // Seed Roles
            if (!context.Roles.Any())
            {
                CreateRole(context, "Administrator");
                CreateRole(context, "User");

                // Seed Roles To Users
                AddUserToRole(context, "yani", "Administrator");
                AddUserToRole(context, "petio", "Administrator");
                AddUserToRole(context, "joro", "Administrator");
                AddUserToRole(context, "pesho", "User");
            }

            // Seed Products
            if (!context.Products.Any())
            {
                // Insert pizzas
                CreateProduct(context, "���������", "", 9.50m, "http://westport.tarrylodge.com/wp-content/uploads/sites/17/2015/08/Babbo_Boston_Food_703-740x380.jpg", ProductType.Pizza);
                CreateProduct(context, "��������", "", 13m, "http://balkan-pizza-doner.de/wp-content/uploads/2015/08/Pepperoni_1.jpg", ProductType.Pizza);
                CreateProduct(context, "������", "", 11m, "https://cdn.media.yp.ca/8621438/pcc-5649004053842719950-IMG-0072_r.JPG", ProductType.Pizza);
                CreateProduct(context, "��������a", "", 12m, "http://www.silviocicchi.com/pizzachef/wp-content/uploads/2015/02/a-evid-672x372.jpg", ProductType.Pizza);
                CreateProduct(context, "Meat Mania", "", 14.50m, "http://68.media.tumblr.com/3413204e7b24a9a690085326cab23e41/tumblr_o6ijpa8Vjx1tg05wco1_1280.jpg", ProductType.Pizza);
                CreateProduct(context, "�����", "", 11m, "https://s3.amazonaws.com/ODNUploads/53c7197f0d5d1hawaiian.jpg", ProductType.Pizza);

                // Insert drinks
                CreateProduct(context, "Coca Cola", "", 1.50m, "https://lh3.googleusercontent.com/-hC_vFNFAy2w/Vp5H1cct9AI/AAAAAAAADGc/Hfz2esqbQkYJ8cLMioumS1GatHfJ4EmwgCJoC/w610-h610/CokeRedDisc.JPG", ProductType.Drink);
                CreateProduct(context, "Coca Cola Zero", "", 1.50m, "http://www.coca-colaproductfacts.com/content/dam/productfacts/us/productDetails/BrandLogos/BL_CokeZero.png", ProductType.Drink);
                CreateProduct(context, "Fanta", "", 1.50m, "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Fanta_logo_global.svg/1200px-Fanta_logo_global.svg.png", ProductType.Drink);
                CreateProduct(context, "��������� ����", "", 1m, "http://www.bankia.bg/content/dam/GO/bonaqua/bulgaria/brand-logos/Bankia_Logo.jpg", ProductType.Drink);
                CreateProduct(context, "Nestea", "", 2m, "https://upload.wikimedia.org/wikipedia/en/thumb/9/9a/Nestea_logo.svg/1200px-Nestea_logo.svg.png", ProductType.Drink);
                CreateProduct(context, "Sprite", "", 2m, "https://www.sprite.com/content/dam/sprite2016/sprite_logo_green2.png", ProductType.Drink);
                CreateProduct(context, "�������", "", 2m, "http://zagorkacompany.bg/uploads/portfolio/zagorkalogowhite.jpg", ProductType.Drink);

                // Insert salads
                CreateProduct(context, "������ ���� ���", "", 4.50m, "http://www.az-jenata.bg/media/az-jenata/files/articles/528x396/5466ce477114fe649d9f5be129fe601a.jpg", ProductType.Salad);
                CreateProduct(context, "������ ����� � ����", "", 5.50m, "http://recipes.dahapna.co.uk/wp-content/uploads/2016/01/%D0%A1%D0%B0%D0%BB%D0%B0%D1%82%D0%B0-%E2%80%9C%D0%A6%D0%B5%D0%B7%D0%B0%D1%80%E2%80%9D-%D1%81-%D0%BF%D0%B8%D0%BB%D0%B5.png", ProductType.Salad);
                CreateProduct(context, "������ ����� � �����", "", 5.50m, "http://tago.bg/wp-content/uploads/2016/03/%D0%A1%D0%B0%D0%BB%D0%B0%D1%82%D0%B0-%D0%A6%D0%B5%D0%B7%D0%B0%D1%80-1-1280x852.jpg", ProductType.Salad);
                CreateProduct(context, "������ ������ �� �����", "", 4.50m, "http://recepti.gotvach.bg/files/lib/600x350/salata-mayo-corn-cabbage.jpg", ProductType.Salad);
                CreateProduct(context, "����� ������ � ������ � ������", "", 3.50m, "http://recepti.gotvach.bg/files/lib/600x350/salata-rukula-cveklo1.jpg", ProductType.Salad);
                CreateProduct(context, "������ ������", "", 4.00m, "http://recepti.gotvach.bg/files/lib/600x350/salata-prolet.jpg", ProductType.Salad);
                CreateProduct(context, "����������� ������", "", 5.00m, "http://recepti.gotvach.bg/files/lib/600x350/mnogosloina-salata.JPG", ProductType.Salad);

                // Insert sandwiches
                CreateProduct(context, "������� � ���� ������, ������ � �������", "", 4.00m, "http://recepti.gotvach.bg/files/lib/600x350/sanvich-pile-spanak2.jpg", ProductType.Sandwich);
                CreateProduct(context, "������������� ������� � ��� ���", "", 3.00m, "http://recepti.gotvach.bg/files/lib/600x350/vegan-mushroom-burger.jpg", ProductType.Sandwich);
                CreateProduct(context, "����������� ������ � ���", "", 4.00m, "http://recepti.gotvach.bg/files/lib/600x350/mexico-burger-bob.jpg", ProductType.Sandwich);
                CreateProduct(context, "������� � ������� � �������", "", 5.00m, "http://recepti.gotvach.bg/files/lib/600x350/guacamole-burrito-chicken.jpg", ProductType.Sandwich);
                CreateProduct(context, "������������� ������� ��������", "", 2.50m, "http://recepti.gotvach.bg/files/lib/600x350/sandvichi-salam.jpg", ProductType.Sandwich);
                CreateProduct(context, "�������� ������� � ��������", "", 1.50m, "http://recepti.gotvach.bg/files/lib/600x350/tosterni-filii-kashkaval.jpg", ProductType.Sandwich);
                CreateProduct(context, "����� ��������", "", 3.50m, "http://recepti.gotvach.bg/files/lib/600x350/parti-sandvichi2456.jpg", ProductType.Sandwich);
            }

            // Seed Ingredients
            if (!context.Ingredinets.Any())
            {
                // Add ingredients to pizzas
                AddIngretientsToProduct(context, "���������", "������� ���", "��������");
                AddIngretientsToProduct(context, "��������", "������� ���", "��������", "��������");
                AddIngretientsToProduct(context, "������", "������� ���", "��������", "���", "������ �����", "������", "����� �������", "����");
                AddIngretientsToProduct(context, "��������a", "������� ���", "��������", "��������", "���� �����", "���");
                AddIngretientsToProduct(context, "Meat Mania", "������� ���", "��������", "������", "�������� �������", "����� �����", "����", "������ �����");
                AddIngretientsToProduct(context, "�����", "������� ���", "��������", "������ �����", "������");

                // Add ingredients to salads
                AddIngretientsToProduct(context, "������ ���� ���", "�������", "���� ���", "��������", "�������", "�����", "������");
                AddIngretientsToProduct(context, "������ ����� � ����", "�������", "����", "��������", "�������", "��������", "����� ���");
                AddIngretientsToProduct(context, "������ ����� � �����", "�������", "�����", "��������", "�������", "��������", "����� ���");
                AddIngretientsToProduct(context, "������ ������ �� �����", "����", "������", "����������", "��������", "��������");
                AddIngretientsToProduct(context, "����� ������ � ������ � ������", "������", "������", "������", "������", "�����");
                AddIngretientsToProduct(context, "������ ������", "������", "����������", "������ ���������", "������ ����", "�����", "������ �� ���", "����� ���", "������ �������", "�������", "������", "������", "����");
                AddIngretientsToProduct(context, "����������� ������", "����", "������", "����", "��������", "�������", "��������", "������ �����", "��������");

                // Add ingredients to sandwiches
                AddIngretientsToProduct(context, "������� � ���� ������, ������ � �������", "������������ ����", "����", "�����", "�����", "�������", "������", "������", "���� ������", "������� ����", "������ ���", "����� �����");
                AddIngretientsToProduct(context, "������������� ������� � ��� ���", "������", "���", "�������", "�������� ����", "���", "������", "�����", "��������", "����� �����");
                AddIngretientsToProduct(context, "����������� ������ � ���", "���", "�����", "������", "�����", "�����", "������", "���", "�����", "�����", "������� �������", "����");
                AddIngretientsToProduct(context, "������� � ������� � �������", "�������", "�����", "�������", "������", "��������", "��������", "������", "����", "����� �����", "���������");
                AddIngretientsToProduct(context, "������������� ������� ��������", "�����", "��������", "����� ������", "������", "����������");
                AddIngretientsToProduct(context, "�������� ������� � ��������", "��������", "�����");
                AddIngretientsToProduct(context, "����� ��������", "����� ������", "�����", "������", "�����", "�������");
            }

            // Insert feedback for product by user
            if (!context.Feedbacks.Any())
            {
                // Add feedbacks for pizzas.
                CreateFeedbackForProduct(context, "���������", "yani", "���� ����� ������ � ����� �������� �� �� ����� ���.");
                CreateFeedbackForProduct(context, "���������", "petio", "�� ������, �� �� �� ������, �� �� ������ #k.");
                CreateFeedbackForProduct(context, "��������", "petio", "���� ����� ����. ��������� �� ���� ���!");
                CreateFeedbackForProduct(context, "��������", "yani", "������� �� �� �����. ���� �������� ����!");
                CreateFeedbackForProduct(context, "��������", "joro", "����� �� ������! ���� �� ��������, ��� �� � ��������.");
                CreateFeedbackForProduct(context, "������", "joro", "����� ������. ����������!");
                CreateFeedbackForProduct(context, "��������a", "yani", "��� �� ����� ������ �����, ����� �� ������ ���� ����� �����.");
                CreateFeedbackForProduct(context, "Meat Mania", "petio", "������ ���� ��������, �� ��������� ��� ���� �����.");
                CreateFeedbackForProduct(context, "�����", "yani", "������� �� � ��-������ �������� ����...");
                CreateFeedbackForProduct(context, "Meat Mania", "pesho", "�����.");
                CreateFeedbackForProduct(context, "�����", "pesho", "�� �����.");
                CreateFeedbackForProduct(context, "������", "pesho", "������� ��.");

                // Add feedbacks for salads.
                CreateFeedbackForProduct(context, "������ ���� ���", "yani", "���������!!!");
                CreateFeedbackForProduct(context, "������ ����� � ����", "petio", "���� �����. ��� �� :)");
            }

            // Insert orders for users 
            // With random chosen products and quantity
            if (!context.Orders.Any())
            {
                for (var i = 0; i < 2; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "joro", 1 + i, 1, 2);
                }

                for (var i = 0; i < 3; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "petio", 1 + i, 1, 2);
                }

                for (var i = 0; i < 4; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "yani", 1 + i, 1, 5);
                }

                for (var i = 0; i < 5; i++)
                {
                    InsertOrderWithRandomOrderProductsForUser(context, "pesho", 1 + i, 1, 3);
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

            var orderDateRandom = new Random();

            for (var i = 0; i < username.Length * username.Length / 2 + 3; i++)
            {
                int mix = orderDateRandom.Next();
            }

            var order = new Order()
            {
                Date = DateTime.Now.AddDays(orderDateRandom.Next(-50, 0)).AddMinutes(orderDateRandom.Next(-800, 0))
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

            for (var i = 0; i < targetProduct.Name.Length * username.Length / 2; i++)
            {
                int mix = rnd.Next();
            }

            var feedback = new Feedback()
            {
                Content = feedbackContent,
                Customer = user,
                CustomerId = user.Id,
                ProductId = targetProduct.Id,
                CreatedOn = DateTime.Now.AddDays(rnd.Next(-10, 0)).AddMinutes(rnd.Next(-300, 0))
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

            foreach (var ingredientName in ingredientNames)
            {
                var targetIngredient = context.Ingredinets.FirstOrDefault(i => i.Name == ingredientName);

                if (targetIngredient == null)
                {
                    targetIngredient = new Ingredient()
                    {
                        Name = ingredientName,
                        Description = string.Empty
                    };
                }

                targetProduct.Ingredients.Add(targetIngredient);
            }

            //var ingredients = context.Ingredinets.Where(i => ingredientNames.Contains(i.Name)).ToList();

            //foreach (var ingredient in ingredients)
            //{
            //    targetProduct.Ingredients.Add(ingredient);
            //}

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

        private void CreateProduct(JPGPizzaDbContext context, string name, string description, decimal price, string picture, ProductType type)
        {
            var product = new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                Picture = picture,
                Type = type
            };

            context.Products.Add(product);
            context.SaveChanges();
        }

        private void CreateUser(JPGPizzaDbContext context, string username, string email, string password, string firstName, string lastName, int age, Gender gender)
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
                UserName = username,
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
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return;
            }

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
