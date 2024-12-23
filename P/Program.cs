using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace P
{
    internal class Program
    {
        static void Main(string[] args)
        {
            user u = new user();
            memmber m = new memmber(u);  // Pass the user instance to member
            librarian lib = new librarian();
            Adminstrator admain = new Adminstrator();
            library_utillity lu = new library_utillity();
            LibraryData  dd = new LibraryData();
            u.dict();
            u.book();
            lib.dict();
            admain.admain_dept();
            admain.address_dept();
            lu.hash();
            u.add_user("samer", "12345566", "samer@gmail.com", 1);
            u.add_user("sameh", "1111111111", "sameh@gmail.com", 2);
            u.add_user("mohammad", "12333555", "mohammad@gmail.com", 3);

            u.book_list.Add("math");
            u.book_list.Add("art");
            u.book_list.Add("geo");
            u.book_list.Add("phys");

            // Debug: Print the books in user to ensure they are added
            Console.WriteLine("Books in user:");
            u.display();

            //Console.WriteLine("Testing book search:");
            //m.search_book("math");  // Search for a specific book

            //Console.WriteLine("Testing user login:");
            //u.login();
            //m.bo();
            //m.return_Book(("math"));
            //Console.WriteLine("authenticate  member:");
            //lib.authenticate_member(1);
            //lib.managed_books("add","math",7);
            //lib.dispaly_lib();
            admain.add_admin("aida", "Hospital_dept");
            admain.add_admin("abass", "help desk support");
            admain.add_admin("sameha", "bank acounting");
            admain.add_address("aida", "Amman");
            admain.add_address("abass", "jerash");
            admain.add_address("sameha", "Amman");
            string[] testEmails = { "example.com", "habeeb@gmail.com", "userss@domain", "user@ssd.com", "user@.com" };
            foreach (var email in testEmails)
            {
                bool isValid = library_utillity.validate_email(email);
                Console.WriteLine($"{email} is {(isValid ? "valid" : "invalid")}");

            }
            lu.add_to_hash();
            lu.pass_word("1111111111");
            Console.WriteLine("contains");
            Console.WriteLine(lu.is_valid("1111111111"));

            Console.WriteLine("--------------------------");
            LibraryData.AddUser(new user {name = "sames"});
            LibraryData.AddUser(new user { name = "ashraf" });
            LibraryData.AddBook("math",7);
            LibraryData.GetBookAvailability("math");

        }
    }

    public interface Ifor
    {
        public void login();
    }

    public abstract class for_identity
    {
        public abstract void display();
    }

    public class user : Ifor
    {
        private int user_id;
        public string name;
        public string email;
        public string password;
        public Dictionary<string, string> data;
        public List<string> book_list;
        public string book_title;
        public int get_id
        {
            get
            {
                return user_id;
            }
        }

        public user()
        {
            data = new Dictionary<string, string>();
            book_list = new List<string>();
        }
        public void book()
        {
            book_list = new List<string>();
        }

        public void dict()
        {
            data = new Dictionary<string, string>();
        }
        public void for_user()
        {
            Console.WriteLine("Borrow or return:");
            string action = Console.ReadLine();
            Console.WriteLine("Select the book you want:");
            string bookName = Console.ReadLine();


            if (book_list == null)
            {
                book_list = new List<string>();
            }

            if (this is memmber members)
            {
                if (members.borrowed_book == null)
                {
                    members.borrowed_book = new List<string>();
                }

                if (action.Equals("Borrow", StringComparison.OrdinalIgnoreCase))
                {
                    if (book_list.Contains(bookName))
                    {
                        book_list.Remove(bookName);
                        members.borrowed_book.Add(bookName);
                        Console.WriteLine($"You have borrowed '{bookName}'.");
                    }
                    else
                    {
                        Console.WriteLine("Book not found in the available list.");
                    }
                }
                else if (action.Equals("Return", StringComparison.OrdinalIgnoreCase))
                {
                    if (members.borrowed_book.Contains(bookName))
                    {
                        members.borrowed_book.Remove(bookName);
                        book_list.Add(bookName);
                        Console.WriteLine($"You have returned '{bookName}'.");
                    }
                    else
                    {
                        Console.WriteLine("You do not have this book borrowed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid action.");
                }
            }
            else
            {
                Console.WriteLine("User is not a member.");
            }
        }


        public virtual void login()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string enteredPassword = Console.ReadLine();

            if (data.TryGetValue(username, out string storedPassword))
            {
                if (enteredPassword == storedPassword)
                {
                    Console.WriteLine("Login successful.");
                    for_user();
                }
                else
                {
                    Console.WriteLine("Incorrect password. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("User not found. Please check your username.");
            }
        }

        public void display()
        {
            Console.WriteLine("Available books:");
            foreach (var book in book_list)
            {
                Console.WriteLine(book);
            }
        }

        public void add_user(string user_name, string passwords, string emails, int user_ids)
        {
            name = user_name;
            email = emails;
            password = passwords;
            user_id = user_ids;

            if (!data.ContainsKey(name))
            {
                data.Add(name, password);
            }
            else
            {
                Console.WriteLine("User already exists.");
            }
        }

        public virtual void update_contact_info(string newEmail)
        {
            email = newEmail;
        }
    }

    public class memmber : user, Ifor
    {
        public List<string> borrowed_book;
        user so = new user();

        public memmber(user userInstance)
        {
            borrowed_book = new List<string>();
            book_list = new List<string>(userInstance.book_list);
        }

        public void bo()
        {
            borrowed_book = new List<string>();
        }
        public void borrowed()
        {
            login();
            if (borrowed_book.Count > 0)
            {
                Console.WriteLine("Borrowed Books:");
                foreach (var book in borrowed_book)
                {
                    Console.WriteLine(book);
                }
            }
            else
            {
                Console.WriteLine("No borrowed books.");
            }
        }

        public void search_book(string bookName)
        {
            bool book_found = book_list.Any(b => b.Equals(bookName, StringComparison.OrdinalIgnoreCase));

            if (book_found)
            {
                Console.WriteLine($"Book '{bookName}' is available in the library.");
            }
            else
            {
                Console.WriteLine($"There is no book named '{bookName}' in the library.");
            }
        }

        public override void login()
        {
            base.login();
        }
        //
        public void borrow_book(string borrow)
        {
        }
        public void return_Book(string bookTitle)
        {
            if (borrowed_book != null && borrowed_book.Contains(bookTitle))
            {
                this.display();
                borrowed_book.Remove(bookTitle);
                so.book_list.Add(bookTitle);
            }
            else
            {
                Console.WriteLine("The book was not found in the borrowed list.");
            }
        }

        public void displays()
        {
            Console.WriteLine(borrowed_book);
        }
    }
    public class librarian : user
    {
        public Dictionary<string, int> managed_book;
        user sh = new user();

        public void dict()
        {
            managed_book = new Dictionary<string, int>();
        }
        public void dispaly_lib()
        {
            foreach (var dict in managed_book)
                Console.WriteLine("keys of value" + dict);
        }
        public void authenticate_member(int member_id)
        {
            base.login();
            Console.WriteLine("what service do you want : retrive data : access to system ?  ");
            string f = Console.ReadLine();
            if (f == "retrive")
            {
                if (member_id.Equals(sh.get_id) && password.Equals(sh.password) && member_id == 1 || member_id == 3)
                {
                    foreach (var t in book_list)
                    {
                        Console.WriteLine(t);
                    }
                }
                else if (f == "access")
                {
                    Console.WriteLine("what operation wanna do :");
                    string h = Console.ReadLine();
                    if (h == "add")
                    {
                        Console.WriteLine("add value !");
                        var g = Console.ReadLine();
                        book_list.Add(g);
                    }
                    else if (h == "remove")
                    {
                        Console.WriteLine("add value !");
                        var g = Console.ReadLine();
                        book_list.Remove(g);
                    }
                    else if (h == "clear")
                    {
                        book_list.Clear();
                    }
                }
            }
        }

        public void managed_books(string action, string book_title, int quantity)
        {
            if (managed_book == null)
            {
                managed_book = new Dictionary<string, int>();
            }
            switch (action.ToLower())
            {
                case "update":
                    if (managed_book.ContainsKey(book_title))
                    {
                        managed_book[book_title] = quantity;
                        Console.WriteLine("successfully updated");
                    }
                    break;
                case "delete":
                    if (managed_book.ContainsKey(book_title))
                    {
                        managed_book.Remove(book_title);
                        Console.WriteLine("successfully Removed");
                    }
                    break;
                case "add":
                    if (managed_book.ContainsKey(book_title))
                    {
                        Console.WriteLine("key is already exist");
                    }
                    else
                    {
                        managed_book.Add(book_title, quantity);
                        Console.WriteLine("successfully added");

                    }
                    break;
                default:
                    Console.WriteLine("Non of choices");
                    break;
            }
        }
        public void generate_report()
        {
            Console.WriteLine("total number of book's :");
            int total_num = book_list.Count;
            Console.WriteLine($"Total number is :{total_num}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("list of book with quantites :");
            var mb = managed_book.Select(book => new { book.Key, book.Value }).ToList();
            Console.WriteLine($"book with quantity {mb}");
            Console.WriteLine("------------------------------------");

            Console.WriteLine("book with low stack :");
            var ms = managed_book.Where(book => book.Value > 5).Select(book => book.Value).ToList();
            Console.WriteLine($"books with hight stock : {ms}");
        }

    }
    public class Adminstrator : user
    {
        public Dictionary<string, string> aadmain_dept;
        public Dictionary<string, string> address;

        public void admain_dept()
        {
            aadmain_dept = new Dictionary<string, string>();
        }
        public void address_dept()
        {
            address = new Dictionary<string, string>();
        }
        public void manage_user_account(string action, user user)
        {
            Console.WriteLine("Option : remove_user , add_user");
            switch (action)
            {
                case "remove_user":
                    user.data.Remove(user.name);
                    Console.WriteLine("remove user success");
                    break;
                case "add_user":
                    if (!data.ContainsKey(user.name))
                    {
                        user.data.Add(user.name, user.password);
                        Console.WriteLine("user_added successfully");
                    }
                    break;
                default:
                    Console.WriteLine("Non of choices");
                    break;
            }
        }
        public void add_admin(string s, string department)
        {
            if (!aadmain_dept.ContainsKey(s) && aadmain_dept == null)
            {
                aadmain_dept.Add(s, department);
            }
        }
        public void add_address(string s, string addresss)
        {
            if (!address.ContainsKey(s) && aadmain_dept != null)
            {
                address.Add(s, addresss);
            }
        }
        public void generate_admin_report(string name)
        {
            Console.WriteLine("Admin name is :");
            var names = aadmain_dept.Where(admin => admin.Key == name).Select(admin => admin.Key);
            Console.WriteLine($"{names}");
            Console.WriteLine("and department work on is :");
            var department = aadmain_dept.Where(admin => admin.Key == name).Select(admin => admin.Value);
            Console.WriteLine($"{department}");
            Console.WriteLine("address of admin :");
            if (aadmain_dept.ContainsKey(name) && address.ContainsKey(name))
            {
                var addres = address.Where(addres => addres.Key == name).Select(addres => addres.Value);
                Console.WriteLine($"adress admain is :{addres}");
            }
            else if (address != null)
            {
                Console.WriteLine($"Address for {name}: {address}");
            }
            else
            {
                Console.WriteLine($"No address found for {name}.");
            }
        }
    }
    public sealed class library_utillity : user
    {
        private HashSet<string> password;
        user users = new user();
        public void hash()
        {
           password = new HashSet<string>();
        }
        public static bool validate_email(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }
        public string pass_word(string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(pass));
            }

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public void add_to_hash()
        {
            password.Add(users.password);
            Console.WriteLine("success_addedd");
        }

        public bool is_valid(string check_password)
        {
            string hashes = pass_word(check_password);
            return hashes.Contains(hashes);
        }

    }

    public  class LibraryData
    {
    
        public static List<user> AllUsers { get; private set; } = new List<user>();
        public static Dictionary<string, int> AllBooks { get; private set; } = new Dictionary<string, int>();

        
        public static void AddUser(user users)
        {
            AllUsers.Add(users);
            Console.WriteLine("User added: " + users.name);
        }

       
        public static void AddBook(string title, int quantity)
        {
            if (AllBooks.ContainsKey(title))
            {
                AllBooks[title] += quantity; 
            }
            else
            {
                AllBooks[title] = quantity; 
            }
            Console.WriteLine($"Book added: {title} (Quantity: {quantity})");
        }

        
        public static int GetBookAvailability(string title)
        {
            if (AllBooks.ContainsKey(title))
            {
                return AllBooks[title]; 
            }
            else
            {
                return 0; 
            }
        }
    }
}