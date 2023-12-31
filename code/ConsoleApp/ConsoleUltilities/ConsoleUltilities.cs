using Persistence;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Threading;

namespace CS
{
    public class Ultilities
    {

        public int MenuHandle(string title1, string[] menuItem, string staffinfo, string[] itemStep, int step)//show menu with information of an object
        {
            ConsoleKeyInfo key;
            int row = 0;
            string item = "";
            do
            {
                Console.Clear();
                Logo();
                if (step != 0)
                {
                    Step(itemStep, step);
                }
                Console.Write(title1);
                Console.Write(@"
                    |                 {0, 103}   |
                    =============================================================================================================================", staffinfo);
                // if(title1 != null)
                //     Title(title1, str1);
                for (int i=0; i < menuItem.Count(); i++)
                {
                    item = addSpaceToStr(menuItem[i], 82);
                    if (row == i)
                    {
                        Console.Write(@"
                    |    ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        // Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(item);
                        Console.Write("                                 ");
                        Console.ResetColor();
                        Console.Write("    |");
                    }else System.Console.Write(@"
                    |    {0}                                     |", item );
                }
                Console.Write(@"
                    =============================================================================================================================
                    {Up Arrow}{Down Arrow} Choice.                                 {Enter} Confirm.                                               ");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.UpArrow && row > 0)
                {
                    row--;
                }else if (key.Key == ConsoleKey.DownArrow && row < menuItem.Count()-1)
                {
                    row++;
                }else if (key.Key == ConsoleKey.Enter)
                {
                    return row+1;
                }
                // else if (key.Key == ConsoleKey.Escape)
                // {
                //     return -1;
                // }
                else if (key.Key == ConsoleKey .Tab)
                {
                    return -1;
                }
            }while (key.Key != ConsoleKey.Tab);
            return 0;
        }

  
        public void Line(){
            System.Console.Write(@"
                    =============================================================================================================================");
            }                         
 

        public string pressEnterTab(string title, string[] menuItem, int choice, string staffInfo, string text, string[] itemStep, int step)
        {
            ConsoleKeyInfo key;
            // int row = 0;
            string item;
            do
            {
                Console.Clear();
                Logo();
                Step(itemStep, step);
                Console.Write(title);
                Console.Write(@"
                    |                 {0, 103}   |
                    =============================================================================================================================", staffInfo);
                // if(title1 != null)
                //     Title(title1, str1);
                for (int i=0; i < menuItem.Count(); i++)
                {
                    item = addSpaceToStr(menuItem[i], 82);
                    if (choice-1 == i)
                    {
                        Console.Write(@"
                    |    ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        // Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(item);
                        Console.Write("                                 ");
                        Console.ResetColor();
                        Console.Write("    |");
                    }else System.Console.Write(@"
                    |    {0}                                     |", item );
                }
                Console.Write(@"
                    =============================================================================================================================
                                      {Enter} Confirm.                                               {Tab} Back.");
                Console.Write(text);
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    return "ENTER";
                }else if (key.Key == ConsoleKey.Tab)
                {
                    return "TAB";
                }
            }while (key.Key != ConsoleKey.Tab && key.Key != ConsoleKey.Enter);
            // if (key.Key == ConsoleKey.Escape) 
            // {
            //     return "ESCAPE"; 
            // }
            return "TAB";

            // string PressKey = "Esc";
            // ConsoleKeyInfo key;
            // Console.WriteLine(text);
            // key = Console.ReadKey(true);
            // do
            // {
            //     if(key.Key!= ConsoleKey.Enter && key.Key != ConsoleKey.Escape)
            //     {
            //         Console.Clear();
            //         Line();
            //         Console.WriteLine(text);
            //         Console.WriteLine("[!]Can only press [Esc] or [Enter].");
            //         key = Console.ReadKey(true);
            //     }
            // } while (key.Key!= ConsoleKey.Enter && key.Key != ConsoleKey.Escape);
            // if (key.Key == ConsoleKey.Enter)PressKey = "Enter";
            // return PressKey;
        }
        public string enterPhone()
        {
            string phone = "";
            int count = 0;
            ConsoleKeyInfo key;
            do
            {
                if(count == 10)
                {
                    return phone;
                }
                key = Console.ReadKey(true);
                if ((key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9))
                {
                    count ++;
                    phone += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                // else if(key.Key == ConsoleKey.Escape){
                //     return "ESCAPE";
                // }
                else if(key.Key == ConsoleKey.Backspace){
                    return "BACKSPACE";
                }
                else
                {
                    Console.Write("");
                }
            }while (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape);
            Console.WriteLine("");
            return phone;
        }

 
        public rowPageSpl updatePageSpl(int No, string Name, int Unit_price, string category)
        {
            rowPageSpl rowPageSpl = new rowPageSpl();
            rowPageSpl.No = No;
            rowPageSpl.Name =Name;
            rowPageSpl.Unit_price =Unit_price;
            rowPageSpl.Category = category;
            return rowPageSpl;
        }
        public List<rowPageSpl> GetListClothesByCategory(List<Clothes> ListClothes, List<Size_color> List_szcl, List<Size> ListSize, List<Color> ListColor, List<Categories> ListCategory, int page, int row, int category_ID)
        {
            Clothes clothes = new Clothes();
            List<rowPageSpl> rowPageSpls = new List<rowPageSpl>();
            rowPageSpl rowpag = new rowPageSpl();
            // string nameColor = "", nameSize = "";
            bool checkIDclothes = true;
            int count = 1;
            string category ="";
            foreach (Clothes Clothes in ListClothes)
            {
                if (Clothes.Category_ID == category_ID)
                {
                    checkIDclothes = true;
                    foreach (rowPageSpl rowPage in rowPageSpls)
                    {
                        if(rowPage.Name == Clothes.Name)
                        {
                            checkIDclothes = false;
                        }
                    }
                    if (checkIDclothes == true)
                    {
                        foreach (Categories item_Category in ListCategory)
                        {
                            if (item_Category.ID == category_ID)
                            {
                                category = item_Category.Category_name;
                                break;
                            }
                        }
                        rowpag = updatePageSpl(count, Clothes.Name, Clothes.Unit_price, category);
                        rowPageSpls.Add(rowpag);
                        count++;
                    }
                }
            }
            return rowPageSpls;
        }
        public List<rowPageSpl> getListClothes(List<Clothes> ListClothes, List<Size_color> List_szcl, List<Size> ListSize, List<Color> ListColor, List<Categories> ListCategory, int page, int row, int category_ID)//List<rowPageSpl>
        {
            Clothes clothes = new Clothes();
            List<rowPageSpl> rowPageSpls = new List<rowPageSpl>();
            rowPageSpl rowpag = new rowPageSpl();
            string category = "";
            bool checkIDclothes = true;
            int count = 1;
            if (category_ID != 0)
            {
                rowPageSpls = GetListClothesByCategory(ListClothes, List_szcl, ListSize,ListColor, ListCategory, page, row, category_ID);
            }else{
                foreach (Clothes Clothes in ListClothes)
                {
                    checkIDclothes = true;
                    foreach (rowPageSpl rowPage in rowPageSpls)
                    {
                        if(rowPage.Name == Clothes.Name)
                        {
                            checkIDclothes = false;
                        }
                    }
                    if (checkIDclothes == true)
                    {
                        foreach (Categories item_Category in ListCategory)
                        {
                            if (item_Category.ID == Clothes.Category_ID)
                            {
                                category = item_Category.Category_name;
                                break;
                            }
                        }
                        rowpag = updatePageSpl(count, Clothes.Name, Clothes.Unit_price, category);
                        rowPageSpls.Add(rowpag);
                        count++;
                    }
                }
            }

            return rowPageSpls;
        }
        
        public string PageSplit(List<rowPageSpl> ListRowPage, List<Clothes> ListClothes, List<Size_color> List_szcl, List<Size> ListSize, List<Color> ListColor, List<Categories> ListCategory, string title, string staffInfo, int category_ID, string[] itemStep, int step)
        {
            ConsoleKeyInfo key;
            string nameClothes="";
            int page=0, row=1, No=1;
            ListRowPage = getListClothes(ListClothes, List_szcl, ListSize, ListColor, ListCategory, page, row, category_ID);
            int maxcount = 0, maxpage =0;
            foreach (rowPageSpl item in ListRowPage)
            {
                if (maxcount == 10)
                {
                    maxpage++;
                    maxcount=0;
                }
                maxcount++;
            }
            do
            {
                Console.Clear();
                Logo();
                Step(itemStep, step);
                Console.Write(title);
                Console.Write(@"
                    |                 {0, 103}   |
                    =============================================================================================================================", staffInfo);
                Console.Write(@"
                    |                                                                                                                           |
                    |       =============================================================================================================       |
                    |       | {0,4} | {1,45} | {2,24} | {3,23} |       |
                    |       =============================================================================================================       |", "No", "Clothes Name", "Category", "Unit Price");
                int count =0;
                foreach (rowPageSpl item in ListRowPage)
                {
                    if(item.No>=10*page+1 && item.No<=(10*page+11))
                    {
                    var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    string price = String.Format(info, "{0:N0}", item.Unit_price);
                        if (item.No == No)
                        {
                            Console.Write(@"
                    |       |");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            // Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(" {0,4} | {1,45} | {2,24} | {3,19} VND ", item.No, item.Name, item.Category, price);
                            Console.ResetColor();
                            Console.Write("|       |");
                            nameClothes = item.Name;
                        }else
                        {
                            Console.Write(@"
                    |       | {0,4} | {1,45} | {2,24} | {3,19} VND |       |", item.No, item.Name, item.Category, price);
                        }
                        count++;
                        if (count == 10)
                        {
                            break;
                        }
                    }
                }
                if (count<10)
                {
                    int count2 = count;
                    while (count2 < 10)
                    {
                        Console.Write(@"
                    |       | {0,4} | {1,45} | {2,24} | {3,23} |       |", "", "", "", "");
                        count2++;
                    }
                }
                Console.Write(@"
                    |       =============================================================================================================       |");
                Console.Write(@"
                    |                                                      [{0, 3}/{1, 3}]                                                            |",page+1, maxpage+1);
                Console.Write(@"
                    |                                                                                                                           |
                    =============================================================================================================================
                    {Left Arrow}{Right Arrow} Choose page.                                         {Up Arrow}{Down Arrow} Choose row.
                    {Enter} Confirm.                                                               {Tab} Back.
                    {P} Payment.");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.RightArrow && page < maxpage)
                {
                    page++;
                    row = 1;
                    No = 10*page + row;
                }else if(key.Key == ConsoleKey.LeftArrow && page > 0)
                {
                    page--;
                    row = 1;
                    No = 10*page + row;
                }else if(key.Key == ConsoleKey.DownArrow && row < count)
                {
                    row++;
                    No++;
                }else if(key.Key == ConsoleKey.UpArrow && row > 1)
                {
                    row--;
                    No--;
                }else if(key.Key == ConsoleKey.Tab)
                {
                    return "TAB";
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    return nameClothes;
                }
                else if(key.Key == ConsoleKey.P)
                {
                    return "C";
                }


            } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Tab);
            // if (No!= 0)
            // {
            //     foreach (rowPageSpl item in ListRowPage)
            //     {
            //         if (item.No == No)
            //         {
            //             foreach (Clothes item_clothes in ListClothes)
            //             {
            //                 if (item.Name == item_clothes.Name)
            //                 {
            //                     ID = item_clothes.ID;
            //                     break;
            //                 }
            //             }
            //             break;
            //         }
            //     }
            // }else
            // {
            //     ID = No;
            // }
            return nameClothes;
        }

        public string OnlyEnterNumber(string text)
        {
            string number = "";
            Console.WriteLine(text);
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if ((key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9))
                {
                    number += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                // else if(key.Key == ConsoleKey.Escape){
                //     return "ESCAPE";
                // }
                else if(key.Key == ConsoleKey.Backspace){
                    Console.Clear();
                    Console.WriteLine(text);
                    number = "";
                }
                else
                {
                    Console.Write("");
                }
            }while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Tab);
            if(number == "")number="1";
            return number;
        }

        public Staff LoginMenu(List<Staff> listStaff)
        {
            Staff staff = new Staff();
            int row = 1;
            string report = "";
            string username = staff.UserName + "_", password = staff.Password, space = "Show password";
            bool showPw = false;
            ConsoleKeyInfo key;
            do
            {
                
                if (row == 1)
                {
                    username = staff.UserName + "_";
                }else if (row == 2)
                {
                    password = password+"_";
                }
                
                if (showPw == false)
                {
                }else if (showPw == true)
                {
                    password = staff.Password;
                }
                Console.Clear();
                Logo();
                Console.Write(@"
                    |                                                                                                                           |
                    |                                 username : {0}                                                    |
                    |                                 password : {1}                                                    |
                    |                                                                                                                           |
                    =============================================================================================================================", "["+addSpaceToStr(limitChar(username, 22, 22), 25)+"]", "["+addSpaceToStr(limitChar(password, 22, 22), 25)+"]");
                    Console.Write(@"
                    [Backspace] Delete text.                      [Enter] Confirm.
                    [Up Arrow][Down Arrow] Choice.                [Space] {0}", space);                            
                                         //    00000000011111111122222222223333333333444444444455555555555666666666677777777778888888889999999999 
                Console.WriteLine(report);
                Thread hidePw = new Thread(()=> {
                    hidePassTitle(username, password, report);
                });
                if(row == 2)
                {
                    // hidePw.Start();
                }
                report = "";
                username = staff.UserName;
                password = hideWord(staff.Password);
                // Console.Clear();
                // Logo();
                // Console.Write(@"
                //     |                                                                                                                           |
                //     |               username : {0}               |
                //     |               password : {1}               |
                //     |                                                                                                                           |
                //     =============================================================================================================================", "["+addSpaceToStr(limitChar(username, 20, 20), 23)+"]", "["+addSpaceToStr(limitChar(password, 20, 20), 23)+"]");
                //     Console.Write(@"
                //     {Backspace} Delete text.                      {Enter} Confirm.
                //     {Up Arrow}{Down Arrow} Choice.  ");                            
                //                          //    00000000011111111122222222223333333333444444444455555555555666666666677777777778888888889999999999 
                // Console.WriteLine(report);
                key = Console.ReadKey(true);
                Thread checkReadkey = new Thread(() => {
                    Thread.Sleep(1000);

                });
                if (key.Key == ConsoleKey.UpArrow && row > 1)
                {
                    row--;
                }else if (key.Key == ConsoleKey.DownArrow && row < 2)
                {
                    row++;
                }else if (key.Key == ConsoleKey.Spacebar)
                {
                    if (showPw == false)
                    {
                        showPw = true;
                        space = "Hide password";
                    }else if (showPw == true)
                    {
                        showPw = false;
                        space = "Show password";
                        
                    }
                }
                if (((key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z) || (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9)) && row == 1)
                {
                    staff.UserName += key.KeyChar;
                }else if (key.Key == ConsoleKey.Backspace && row == 1 && staff.UserName.Length > 0)
                {
                    int delete = staff.UserName.Length-1;
                    staff.UserName = staff.UserName.Substring(0, delete);
                }else if (((key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z) || (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9)) && row == 2)
                {
                    staff.Password += key.KeyChar;
                    password += key.KeyChar;
                }else if (key.Key == ConsoleKey.Backspace && row == 2 && staff.Password.Length > 0)
                {
                    // int delete = staff.Password.Length-1;
                    // staff.Password = staff.Password.Substring(0, delete); delete char

                    staff.Password = ""; // delete all
                    password = "";
                }
                else if (key.Key == ConsoleKey.Enter && row == 2 && staff.UserName.Count() >= 6 && staff.Password.Count() >= 6)
                {
//<><><><><><><><><><><><><><><><><><><><><><><><>
                    // byte array representation of that string
                    byte[] encodedPassword = new UTF8Encoding().GetBytes(staff.Password);

                    // need MD5 to calculate the hash
                    byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

                    // string representation (similar to UNIX format)
                    string encoded = BitConverter.ToString(hash)
                    // without dashes
                    .Replace("-", string.Empty)
                    // make lowercase
                    .ToLower();

                    // encoded contains the hash you want
//<><><><><><><><><><><><><><><><><><><><><><><><>
                    foreach (Staff item in listStaff)
                    {
                        if (staff.UserName == item.UserName && encoded == item.Password)
                        {
                            return item;
                        }
                    }
                    report = @"
                    [!]{username} or {password} incorrect.";
                }
                else if (key.Key == ConsoleKey.Enter && row ==1)
                {
                    row++;
                }
                else if (key.Key ==  ConsoleKey.Tab)
                {
                    staff.Password = "";
                    staff.UserName = "";
                    return staff;
                }else if (staff.UserName.Count()<6 || staff.Password.Count()<6 && row ==2)
                {
                    report =@"
                    [!] username and password at least 6 characters.";
                }
                // else if (staff.UserName.Count() == 6 && staff.Password.Count() == 6)report="";
            } while (key.Key != ConsoleKey.Tab);
            staff.Password = "";
            staff.UserName = "";
            return staff;
        }

        public void userManual()
        {
            // Console.Write(@"
            //         | {Esc} Exit.                   {Enter} Complete.              {Back Space ⇽  } Delete text.                                |
            //         | {◀️  ▶️  🔽 🔼 } choice.                                                                                                    |
            //         =============================================================================================================================");
            Console.Write(@"
                        {Esc} Exit / Cancel.                   {Enter} Complete / Confirm.              {Back Space ⇽  } Delete text.
                        {Tab} Back.                            {◀️  ▶️  🔽 🔼 } choice.
");
        }

 
        
        public string hideWord(string word)
        {
            string hideWord = "";
            for (int i = 0; i < word.Length; i++)
            {
                hideWord += "*";
            }
            return hideWord;
        }

        public void hidePassTitle(string username, string password, string report)
        {
            Console.Clear();
            Thread.Sleep(50);
            Logo();
            Console.Write(@"
                    |                                                                                                                           |
                    |                                 username : {0}                                                    |
                    |                                 password : {1}                                                    |
                    |                                                                                                                           |
                    =============================================================================================================================", "["+addSpaceToStr(limitChar(username, 22, 22), 25)+"]", "["+addSpaceToStr(limitChar(hideWord(password)+"_", 22, 22), 25)+"]");
            Console.Write(@"
                    {Backspace} Delete text.                      {Enter} Confirm.
                    {Up Arrow}{Down Arrow} Choice.  ");                            
                                         //    00000000011111111122222222223333333333444444444455555555555666666666677777777778888888889999999999 
            Console.WriteLine(report);
        }

        public string addSpaceToStr(string str, int a)
        {
            int spaceCount =  a-str.Length;
            string strSpace = str;
            for (int i = 0; i < spaceCount; i++)
            {
                strSpace = strSpace + " ";
            }
            return strSpace;
        }

        public Customer enterPhoneCustomer(List<Customer> listCustomer, string staffInfo, string[] itemStep, int step)
        {
            Customer customer = new Customer();
            string phoneNum ="";
            string report = "";
            bool check = true;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                phoneNum = customer.PhoneNumber;
                Logo();
                Step(itemStep, step);
                Console.Write(@"
                    |                                                                                                                           |
                    |                                            ---- Creter New Order ----                                                     |
                    |                 {0, 103}   |
                    =============================================================================================================================
                    |                                                                                                                           |
                    |               Phone Number : {1}           |
                    |                                                                                                                           |
                    =============================================================================================================================",staffInfo, addSpaceToStr("["+addSpaceToStr(phoneNum+"_", 11)+"]", 82));
                    Console.Write(@"
                                                {Backspace} Delete text.                    {Tab} Back.  ");
                                         //    00000000011111111122222222223333333333444444444455555555555666666666677777777778888888889999999999 
                    // userManual();
                    // report = report + @"
                    // [!] Enter phone customer.";
                Console.Write(@"
                    {0}", report);
                report = "";
                key = Console.ReadKey(true);
                if (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9 && customer.PhoneNumber.Length < 10)
                {
                    customer.PhoneNumber += key.KeyChar;
                }else if (key.Key == ConsoleKey.Backspace && customer.PhoneNumber.Length > 0 && customer.PhoneNumber.Length < 10)
                {
                    int delete = customer.PhoneNumber.Length-1;
                    customer.PhoneNumber = customer.PhoneNumber.Substring(0, delete);
                }else if (key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z && customer.PhoneNumber.Length < 10)
                {
                    report = @"
                    [!] Only enter number.";
                }
                else if (key.Key == ConsoleKey.Enter && customer.PhoneNumber.Length == 10)
                {
                    foreach (Customer item in listCustomer)
                    {
                        if (customer.PhoneNumber == item.PhoneNumber)
                        {
                            return item;
                        }
                    }
                    check = false;
                }
                else if(key.Key == ConsoleKey.Backspace && customer.PhoneNumber.Length >= 10)
                {
                    customer.PhoneNumber ="";
                }
                // else if (key.Key == ConsoleKey.Escape)
                // {
                //     customer.Name = "ESCAPE";
                //     return customer;
                // }
                else if (key.Key == ConsoleKey.Tab)
                {
                    customer.Name = "TAB";
                    return customer;
                }
                // else
                // phoneNum = customer.PhoneNumber + "_";
                 if (customer.PhoneNumber.Length == 10)
                {
                    report = @"
                    [?] User phone number "+ customer.PhoneNumber + @".
                    {Enter} Confirm.
                    {Back space} Re-Enter.";
                }
            } while (check == true);
            customer.Name = "";
            return customer;
        }

        public string newCustomer(string staff, string phoneNumber, string report, string[] itemStep, int step)
        {
            ConsoleKeyInfo key;
            string namecustomer="";
            // string text = "";
            do
            {
                Console.Clear();
                Logo();
                Step(itemStep, step);
                Console.Write(@"
                    |                                                                                                                           |
                    |                                                ---- New Customer ----                                                     |
                    |                 {0, 103}   |
                    =============================================================================================================================
                    |                                                                                                                           |
                    |                 Phone Number   : {1, 10}                                                                             |
                    |                 Name Customer  : {2}         |
                    |                                                                                                                           |
                    =============================================================================================================================", staff, "["+phoneNumber+"]", addSpaceToStr("["+addSpaceToStr(limitChar(namecustomer+"_", 10, 10), 12)+"]", 80));
                    Console.Write(@"
                    {Tab} Back.                                                                                      {Backspace} Delete text");
                Console.Write(@"
                    {0}", report);
                report = "";
                key = Console.ReadKey(true);
                if (((key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z) || (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9) || key.Key == ConsoleKey.Spacebar) && namecustomer.Length < 80)
                {
                    namecustomer += key.KeyChar;
                }else if (key.Key == ConsoleKey.Enter && namecustomer != "")
                {
                    return namecustomer;
                }else if (key.Key == ConsoleKey.Enter && namecustomer == "")
                {
                    report = "[!] ERROR! Please enter name customer.";
                }else if (key.Key == ConsoleKey.Backspace && namecustomer.Length > 0)
                {
                    int delete = namecustomer.Length-1;
                    namecustomer = namecustomer.Substring(0, delete);
                }else if (key.Key == ConsoleKey.Tab)
                {
                    return "TAB";
                }
                
            } while (key.Key != ConsoleKey.Tab);
            return "TAB";
        }

        public string showInfoClothes(int ID, List<Categories> categories, List<Clothes> clothesList, List<Size_color> szclList, List<Size> size, List<Color> color, string[] itemStep, int step)
        {
            string OrderQuantity = "1";
            string text = @"
                    {Enter} To enter the quantity                                  {Tab} Back.
                    [!] Default = 1.";
            string report = "";
            int count = 0;
            Clothes clothes = new Clothes();
            ConsoleKeyInfo key;
            foreach (Clothes item in clothesList)
            {
                if (item.ID == ID)
                {
                    clothes = item;
                    break;
                }
            }
            string sizeName ="Size", colorName="color", categoryName="Category";
            int quantity = 0;
            foreach (Categories item in categories)
            {
                if (item.ID == clothes.Category_ID)
                {
                    categoryName = item.Category_name;
                    break;
                }
            }
            foreach (Size_color item in szclList)
            {
                if (item.clothes_ID == clothes.ID)
                {
                    foreach (Size item_size in size)
                    {
                        if (item.Size_ID == item_size.Size_ID)
                        {
                            sizeName = item_size.Size_Name;
                            break;
                        }
                    }
                    foreach (Color item_color in color)
                    {
                        if (item.Color_ID == item_color.Color_ID)
                        {
                            colorName = item_color.Color_Name;
                            break;
                        }
                    }
                    quantity = item.Quantity;
                    break;
                }
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            string price = String.Format(info, "{0:N0}", clothes.Unit_price);
            do
            {
            Console.Clear();
            Logo();
            Step(itemStep, step);
            Console.Write(@"
                    |                                                                                                                           |
                    |                                               ---- Info Clothes ----                                                      |
                    |                                                                                                                           |
                    =============================================================================================================================
                    |                                                                                                                           |
                    |       =============================================================================================================       |
                    |       |                                                                                                           |       |
                    |       |  ID          : {0}  |       |
                    |       |  Name        : {1}  |       |
                    |       |  Size        : {2}  |       |
                    |       |  Color       : {3}  |       |
                    |       |  Category    : {4}  |       |
                    |       |  Material    : {5}  |       |
                    |       |  Quantity    : {6}  |       |
                    |       |  Price       : {7}  |       |
                    |       |  User Manual : {8}  |       |
                    |       |                                                                                                           |       |  
                    |       =============================================================================================================       |
                    |                                                                                                                           |
                    =============================================================================================================================", addSpaceToStr(Convert.ToString(ID), 89), addSpaceToStr(clothes.Name, 89), addSpaceToStr(sizeName, 89), addSpaceToStr(colorName, 89), addSpaceToStr(categoryName, 89), addSpaceToStr(clothes.Material, 89), addSpaceToStr(Convert.ToString(quantity), 89), addSpaceToStr(price + " VND", 89), addSpaceToStr(clothes.user_manual, 89));
                if (count == 1)
                {
                    Console.Write(@"
                    |       Quantity : [" + addSpaceToStr(OrderQuantity+"_]", 97) + @"       |
                    =============================================================================================================================");
                }
                if (OrderQuantity != "" && quantity > 0)
                {
                    if (Convert.ToInt32(OrderQuantity) > quantity )
                    {
                        report = report +@"
                    [!] The order quantity exceeds the quantity of the clothes.";
                    }
                }else if (quantity <= 0)
                    {
                        report = @"
                    [!] Out of stock.";
                        OrderQuantity = "";
                    }
                Console.Write(text);
                // userManual();
                Console.Write(report);
                report = "";
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter && OrderQuantity == "1")
                {
                    OrderQuantity = "";
                    count =1;
                }
                // else if (key.Key == ConsoleKey.Escape)
                // {
                //     return "ESCAPE";
                // }
                else if ((key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9) && OrderQuantity.Length <= 3 && count ==1)
                {
                    OrderQuantity += key.KeyChar;
                }else if ((key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9) && OrderQuantity.Length > 3 && count ==1)
                {
                    report = @"
                    [!] Max value!";
                }
                else if (key.Key == ConsoleKey.Backspace && OrderQuantity.Length > 0 && count ==1)
                {
                    int delete = OrderQuantity.Length - 1;
                    OrderQuantity = OrderQuantity.Substring(0, delete);
                }else if (key.Key == ConsoleKey.Enter && count == 1)
                {
                    if (OrderQuantity == "" && quantity > 0)
                    {
                        return "1";
                    }else if (OrderQuantity != "" && Convert.ToInt32(OrderQuantity) <= quantity && quantity > 0)
                    {
                        return OrderQuantity;
                    }else if (quantity <= 0)
                    {
                        report = @"
                    [!] Out of stock.";
                        OrderQuantity = "";
                    }
                }else if (key.Key == ConsoleKey.Tab)
                {
                    return "TAB";
                }
                // else if (key.Key == ConsoleKey.Escape)
                // {
                //     return "BACK";
                // }
            } while (key.Key != ConsoleKey.Tab);
            return "TAB";
        }

      

        public List<OrderDetails> ShowOrderDetails(Order order, List<OrderDetails> ListOrderDetail, List<Clothes> listClothes, List<Size_color> listSzcl, List<Size> listSize, List<Color> listColor,List<Categories> listCategory, string CustomerName, string CustomerPhone, string NameStaff, int status, string[] itemStep, int step)
        {
            ConsoleKeyInfo key;
            int count = 1, row = 1, maxRow = 0;
            string ClothesName ="";
            int price, ID = 0, TotalPrice;
            int check = 1;
            string size="", color="", category="", name="";
            string paymentMenthod = ""+ order.PaymentMethod;
            string CKey = "Continue";
            Clothes clothes = new Clothes();
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            if (order.PaymentMethod != "")
            {
                CKey = "Confirm Order.";
            }
            do
            {
                TotalPrice = 0;
                price = 0;
                check = 0;
                foreach (OrderDetails item in ListOrderDetail)
                {
                    if (item.Quantity != 0)
                    {
                        check = 1;
                        break;
                    }
                }
                if (check == 0)
                {
                    return ListOrderDetail;
                }
                Console.Clear();
                // row = 1;
                if (status == 1)
                {
                    row=0;
                }
                maxRow=0;
                Logo();
                Step(itemStep, step);
                Console.Write(@"
                    |                                                                                                                           |
                    |                                            ---- Show Order Detail ----                                                    |
                    =============================================================================================================================
                    |                                                                                                                           |
                    |       ▒███████▒ ╔═╗┬  ┌─┐┌┬┐┬ ┬┌─┐┌─┐                                 ╔╗   ╦  ╦    ╦                                      |
                    |       ▒ ▒ ▒ ▄▀░ ║  │  │ │ │ ├─┤├┤ └─┐                                 ╠╩╗  ║  ║    ║                                      |
                    |       ░ ▒ ▄▀▒░  ╚═╝┴─┘└─┘ ┴ ┴ ┴└─┘└─┘                                 ╚═╝  ╩  ╩═╝  ╩═╝                                    |
                    |         ▄▀▒   ░                                                                                                           |
                    |       ▒███████▒                                                                                                           |
                    |       ░▒▒ ▓░▒░▒                                                       Order ID: {0, 3}                                       |
                    |       ░░▒ ▒ ░ ▒                                                                                                           |
                    |       ░ ░ ░ ░ ░                                                                                                           |
                    |         ░ ░                                                                                                               |
                    |       ░         [Thời gian: {4}]                                                       |
                    |                                                                                                                           |
                    |                                                                                                                           |
                    |                 Customer Name   : {1}                                                                    |
                    |                 Phone Number    : {2}                                                                    |
                    |                 Create By Staff : {3}                                                                    |
                    |                 Payment method  : {5}                                                  |
                    |                                                                                                                           |
                    |                                                                                                                           |
                    |       -------------------------------------------------------------------------------------------------------------       |", order.OrderID, addSpaceToStr(CustomerName, 20), addSpaceToStr(CustomerPhone, 20), addSpaceToStr(NameStaff, 20), addSpaceToStr(Convert.ToString(order.CreationTime), 38), addSpaceToStr(paymentMenthod, 38));
                    Console.Write(@"
                    |       | {0, 3} | {1, 39} | {2, 15} | {3, 18} | {4, 18} |       |
                    |       -------------------------------------------------------------------------------------------------------------       |", "No", "Name Clothes", "Quantity", "Unit Price", "Price");
                foreach (OrderDetails item in ListOrderDetail)
                {
                    if (item.Quantity != 0)
                    {
                        maxRow++;
                    }
                }
                count = 1;
                foreach (OrderDetails orderDetails in ListOrderDetail)
                {
                    
                    foreach (Clothes item in listClothes)
                    {
                        if (item.ID == orderDetails.ClothesID)
                        {
                            ClothesName = item.Name;
                            break;
                        }
                    }
                    price = orderDetails.Quantity * orderDetails.UnitPrice;
                    TotalPrice = TotalPrice + price;
                    string stringPrice = String.Format(info, "{0:N0}", price);
                    string stringUnitPrice = String.Format(info, "{0:N0}", orderDetails.UnitPrice);
                    if (orderDetails.Quantity != 0)
                    {
                        if (count == row)
                        {
                            Console.Write(@"
                    |       |");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            // Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(" {0, 3} | {1, 39} | {2, 15} | {3, 14} VND | {4, 14} VND ", count, ClothesName, orderDetails.Quantity, stringUnitPrice, stringPrice);
                            Console.ResetColor();
                            Console.Write(@"|       |");
                            ID = orderDetails.ClothesID;
                        }else
                        {
                            Console.Write(@"
                    |       | {0, 3} | {1, 39} | {2, 15} | {3, 14} VND | {4, 14} VND |       |", count, ClothesName, orderDetails.Quantity, stringUnitPrice, stringPrice);
                        }
                        count++;
                    }
                    if (count > maxRow)
                    {
                        break;
                    }
                }
                string strTotalPrice = String.Format(info, "{0:N0}", TotalPrice);
                Console.Write(@"
                    |       -------------------------------------------------------------------------------------------------------------       |
                    |       | Total Price                                                                          | {0, 14} VND |       |
                    |       -------------------------------------------------------------------------------------------------------------       |", strTotalPrice);
                    if (status != 1)
                    {
                Console.Write(@"
                    |                                                                                                                           |
                    =============================================================================================================================
                                [Tab] Back.                                             [Enter] Edit the number of selected clothes.
                                [Delete] Remove select clothes from order.              [Up Arrow][Down Arrow] Choose clothes. 
                                [C] {0}                                        [X] Cancel order", addSpaceToStr(CKey, 12));
                        
                    }else if (status == 1)
                    {
                        
                Console.Write(@"
                    |                                                                                                                           |
                    =============================================================================================================================
                                [C] {0}                                           [X] Cancel order", addSpaceToStr(CKey, 12));
                    }
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && row > 1 && status != 1)
                {
                    row--;
                }else if(key.Key == ConsoleKey.DownArrow && row < maxRow && status != 1)
                {
                    row++;
                }else if (key.Key == ConsoleKey.Enter && status != 1)
                {
                    foreach (Clothes item in listClothes)
                    {
                        if (item.ID == ID)
                        {
                            foreach (Categories itemCategory in listCategory)
                            {
                                if (itemCategory.ID == item.Category_ID)
                                {
                                    category = itemCategory.Category_name;
                                }
                            }
                            name = item.Name;
                            break;
                        }
                    }
                    foreach (Size_color item in listSzcl)
                    {
                        if (item.clothes_ID == ID)
                        {
                            foreach (Size itemSize in listSize)
                            {
                                if (itemSize.Size_ID == item.Size_ID)
                                {
                                    size = itemSize.Size_Name;
                                    break;
                                }
                            }
                            foreach (Color itemColor in listColor)
                            {
                                if (itemColor.Color_ID == item.Color_ID)
                                {
                                    color = itemColor.Color_Name;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    foreach (OrderDetails item in ListOrderDetail)
                    {
                        if (item.ClothesID == ID)
                        {
                            int quantity = item.Quantity;
                            item.Quantity = UpdateOrderDetail(ListOrderDetail, ID, size, color, category, name, listSzcl, itemStep, 5);
                            foreach (Size_color item_szcl in listSzcl)
                            {
                                if (item_szcl.clothes_ID == ID)
                                {
                                    item_szcl.Quantity += quantity - item.Quantity;
                                }
                            }
                            break;
                        }
                    }
                }else if (key.Key == ConsoleKey.Delete)
                {
                    foreach (OrderDetails item in ListOrderDetail)
                    {
                        if (item.ClothesID == ID)
                        {
                            item.Quantity = 0;
                            break;
                        }
                    }
                }else if (key.Key == ConsoleKey.Tab && status != 1)
                {
                    return null;
                }else if (key.Key == ConsoleKey.C)
                {
                    return ListOrderDetail;
                }else if (key.Key == ConsoleKey.X)
                {
                    List<OrderDetails> Cancel = new List<OrderDetails>();
                    return Cancel;
                }
                

            } while (key.Key != ConsoleKey.C);
            return ListOrderDetail;
        }
        public int UpdateOrderDetail(List<OrderDetails> ListOrderDetail, int ClothesID, string size, string color, string category, string name, List<Size_color> listszcl, string[] itemStep, int step)
        {
            int quantity =1, Quantity=0;
            int Length =0;
            string report =@"
                    [!] Press {Enter} to update quantity of clothes";
            foreach (OrderDetails orderDetail in ListOrderDetail)
            {
                if (ClothesID == orderDetail.ClothesID)
                {
                    string strOrderID = Convert.ToString(orderDetail.OrderID);
                    string strClothesID = Convert.ToString(orderDetail.ClothesID);
                    quantity = orderDetail.Quantity;
                    int count = 0;
                    string strQuantity = Convert.ToString(quantity);

                    strOrderID = addSpaceToStr(strOrderID, 89);
                    strClothesID = addSpaceToStr(strClothesID, 89);
                    size = addSpaceToStr(size, 89);
                    color = addSpaceToStr(color, 89);
                    category = addSpaceToStr(category, 89);
                    name = addSpaceToStr(name, 89);
                    string unitprice = orderDetail.UnitPrice.ToString();
                    string price = addSpaceToStr(unitprice, 89);


                    Length = strQuantity.Length;

                    ConsoleKeyInfo key;

                    do
                    {
                        report = "";
                        // if (strQuantity == "0")
                        // {
                        //     strQuantity = "";
                        // }
                        string strQuantitySpace = addSpaceToStr("["+strQuantity+"]", 89);
                        Console.Clear();
                        Logo();
                Step(itemStep, step);
                        Console.Write(@"
                    |                                                                                                                           |
                    |                                          ---- Update Order Detail ----                                                    |
                    =============================================================================================================================
                    |                                                                                                                           |
                    |       =============================================================================================================       |
                    |       |                                                                                                           |       |
                    |       |  ID          : {0}  |       |
                    |       |  Name        : {1}  |       |
                    |       |  Size        : {2}  |       |
                    |       |  Color       : {3}  |       |
                    |       |  Category    : {4}  |       |
                    |       |  Material    : {5}  |       |
                    |       |  Quantity    : {6}  |       |
                    |       |  Price       : {7}  |       |
                    |       |  User Manual : {8}  |       |
                    |       |                                                                                                           |       |  
                    |       =============================================================================================================       |
                    |                                                                                                                           |
                    =============================================================================================================================
",  strOrderID, strClothesID, name, size, color, category,addSpaceToStr("", 89) ,strQuantitySpace, price, addSpaceToStr("", 89));
                        // if (strQuantity == "")
                        // {
                        //     strQuantity = "0";
                        // }
                        if (strQuantity != "")
                        {
                            foreach (Size_color item in listszcl)
                            {
                                if (ClothesID == item.clothes_ID)
                                {
                                    Quantity = item.Quantity;
                                    break;
                                }
                            }
                            // if (Convert.ToInt32(strQuantity) > Quantity)
                            // {
                            // }
                        }
                        if (strQuantity.Length  >= 4)
                        {
                            report = @"
                    [!] Max value.";
                        }
                        if (strQuantity != "")
                        {
                            if (Convert.ToInt32(strQuantity) > Quantity + quantity)
                            {
                                report = report +@"
                    [!] The order quantity exceeds the quantity of the clothes.";
                            }
                        }
                    Console.Write(report);
                        key = Console.ReadKey(true);
                        if (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9 && Length < 80 && strQuantity.Length  <= 3)
                        {
                            strQuantity += key.KeyChar;
                            Length++;
                        }else if (key.Key == ConsoleKey.Backspace && Length > 1)
                        {
                            count = strQuantity.Length;
                            strQuantity = strQuantity.Substring(0, count-1);
                            Length--;
                        }else if (key.Key == ConsoleKey.Backspace && Length <= 1 && Length >= 0)
                        {
                            strQuantity = "";
                        }else if (key.Key == ConsoleKey.Enter && strQuantity != "")
                        {
                            if (Convert.ToInt32(strQuantity) <= Quantity + quantity)
                            {
                                return Convert.ToInt32(strQuantity);
                            }
                        }else if (key.Key == ConsoleKey.Enter && strQuantity == "")
                        {
                            return orderDetail.Quantity;
                        }
                    } while (key.Key != ConsoleKey.Tab);
                    // if (strQuantity == "")
                    // {
                    //     quantity = 1;
                    // }else quantity = Convert.ToInt32(strQuantity);
                    quantity = orderDetail.Quantity;
                    
                }
            }
            return quantity;
        }

        public int choiceCategory(List<Categories> listCategory, string staffInfo, string[] itemStep, int step)
        {
            int choice = 1, row; 
            // int count =0;
            int category_ID =0;
            ConsoleKeyInfo key;
            do
            {
                row = 1;
                Console.Clear();
                Logo();
                Step(itemStep, step);
                Console.Write(@"
                    |                                                                                                                           |
                    |                                          ---- Choose Category Menu ----                                                   |
                    |                 {0, 103}   |
                    |                                                                                                                           |
                    =============================================================================================================================", staffInfo);
                    foreach (Categories item in listCategory)
                    {
                        if (choice == row)
                        {
                        Console.Write(@"
                    |    ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        // Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write( addSpaceToStr(item.Category_name, 95));
                        Console.Write("                    ");
                        Console.ResetColor();
                        Console.Write("    |");
                        category_ID = item.ID;
                        }else
                        {
                         Console.Write(@"
                    |    {0}                      |", addSpaceToStr(item.Category_name, 97) );
                        }
                        row++;
                    }
                Console.Write(@"
                    =============================================================================================================================
                     {Up Arrow}{Down Arrow} Choice.                            {Enter} Confirm.                            {Tab} Back");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && choice > 1)
                {
                    choice--;
                }else if (key.Key == ConsoleKey.DownArrow && choice < row-1)
                {
                    choice++;
                }else if (key.Key == ConsoleKey.Enter)
                {
                    return category_ID;
                }
                // else if (key.Key == ConsoleKey.Escape)
                // {
                //     return -1;
                
                // }
                else if (key.Key == ConsoleKey.Tab)
                {
                    return 0;
                }
            } while (key.Key != ConsoleKey.Enter);
            return choice;
        }

     

        public int choiceClothesBySzcl(string nameClothes, List<Clothes> listClothes, List<Size_color> listSzcl, List<Size> listSize, List<Color> listColor, string staffInfo, string[] itemStep, int step)
        {
            int ID=0, row =1, no =1;
            int count = 0;
            int quantity = 0;
            string sizeName="", colorName = "";
            ConsoleKeyInfo key;
            do
            {
                no = 1;
                Console.Clear();
                Logo();
                Step(itemStep, step);
                Console.Write(@"
                    |                                                                                                                           |
                    |                                    ---- List Clothes With Size And Color----                                              |
                    |                 {0, 103}   |
                    =============================================================================================================================", staffInfo);
                Console.Write(@"
                    |                                                                                                                           |
                    |       =============================================================================================================       |
                    |       | {0,5} | {1,35} | {2,9} | {3,10} | {4,16} | {5,15} |       |
                    |       =============================================================================================================       |", "ID", "Clothes Name", "Size", "Color", "Quantity", "Unit Price");
                count =0;
                foreach (Clothes clothes in listClothes)
                {
                    if (clothes.Name == nameClothes)
                    {
                        foreach (Size_color szcl in listSzcl)
                        {
                            if (clothes.ID == szcl.clothes_ID)
                            {
                                quantity = szcl.Quantity;
                                foreach (Size size in listSize)
                                {
                                    if (szcl.Size_ID == size.Size_ID)
                                    {
                                        sizeName = size.Size_Name;
                                        break;
                                    }
                                }
                                foreach (Color color in listColor)
                                {
                                    if (szcl.Color_ID == color.Color_ID)
                                    {
                                        colorName = color.Color_Name;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    // if(item.No>=10*page+1 && item.No<=(10*page+11))
                    // {
                    var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    string price = String.Format(info, "{0:N0}", clothes.Unit_price);
                        if (row == no)
                        {
                            Console.Write(@"
                    |       |");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            // Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(" {0,5} | {1,35} | {2,9} | {3,10} | {4,16} | {5,11} VND ", clothes.ID, clothes.Name, sizeName, colorName, quantity, price);
                            Console.ResetColor();
                            Console.Write("|       |");
                            ID=clothes.ID;
                        }else
                        {
                            Console.Write(@"
                    |       | {0,5} | {1,35} | {2,9} | {3,10} | {4,16} | {5,11} VND |       |", clothes.ID, clothes.Name, sizeName, colorName, quantity, price);
                        }
                        no++;
                        count++;
                    // }
                    
                        // break;
                    }
                }
                if (count<10)
                {
                    int count2 = count;
                    while (count2 < 10)
                    {
                        Console.Write(@"
                    |       | {0,5} | {1,35} | {2,9} | {3,10} | {4,16} | {5,15} |       |", "", "", "", "", "", "");
                        count2++;
                    }
                }
                Console.Write(@"
                    |       =============================================================================================================       |");
                // Console.Write(@"
                //     |                                                      [{0, 3}/{1, 3}]                                                            |",page+1, maxpage+1);
                Console.Write(@"
                    |                                                                                                                           |
                    =============================================================================================================================
                    {Left Arrow}{Right Arrow} Choose page.                                         {Up Arrow}{Down Arrow} Choose row.
                    {Enter} Confirm.                                                               {Tab} Back.
                    {P} Payment.");
                key = Console.ReadKey(true);if(key.Key == ConsoleKey.DownArrow && row < count)
                {
                    row++;
                }else if(key.Key == ConsoleKey.UpArrow && row > 1)
                {
                    row--;
                }else if(key.Key == ConsoleKey.Tab)
                {
                    return 0;
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    return ID;
                }
                else if(key.Key == ConsoleKey.P)
                {
                    return -1;
                }


            } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Tab);
            // if (no!= 0)
            // {
            //     foreach (rowPageSpl item in ListRowPage)
            //     {
            //         if (item.No == No)
            //         {
            //             foreach (Clothes item_clothes in ListClothes)
            //             {
            //                 if (item.Name == item_clothes.Name)
            //                 {
            //                     ID = item_clothes.ID;
            //                     break;
            //                 }
            //             }
            //             break;
            //         }
            //     }
            // }else
            // {
            //     ID = No;
            // }
            return ID;
        }

      

        public void Step(string[] itemmenu, int step)
        {
            int count = 0;
            Console.Write(@"
                    |        |");
            foreach (string item in itemmenu)
            {
                if (count == 0 && count == step-1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    // Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("| {0,13} |", item);
                    Console.ResetColor();
                }else if(count == 0)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Blue;
                    // Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("| {0,13} |", item);
                }else if (count < step-1)
                {
                    Console.Write("| {0,13} |", item);
                }else if (count == step-1)
                {
                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    // Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("| {0,13} |", item);
                    Console.ResetColor();
                }else
                {
                    Console.Write("| {0,13} |", item);
                }
                count++;
            }
            Console.ResetColor();
            Console.Write("|         |");
            Line();

        }

        public void ConfirmOrder()
        {
            ConsoleKeyInfo key;
            Console.Clear();
            Logo();
            Console.Write(@"
                    |                                                                                                                           |
                    |                                [!] Order has been successfully uploaded to the database                                   |
                    |                                                                                                                           |
                    =============================================================================================================================
                    Press {Enter} to back Main menu");
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Enter);
        }
        public void Logo()
        {
            Console.Write(@"
                    =============================================================================================================================
                    |                                                                                                                           |
                    |                                       ╔═╗┬  ┌─┐┌┬┐┬ ┬┬┌┐┌┌─┐  ╔═╗┬ ┬┌─┐┌─┐                                                |
                    |                                       ║  │  │ │ │ ├─┤│││││ ┬  ╚═╗├─┤│ │├─┘                                                |
                    |                                       ╚═╝┴─┘└─┘ ┴ ┴ ┴┴┘└┘└─┘  ╚═╝┴ ┴└─┘┴                                                  |
                    |                                                                                                                           |
                    =============================================================================================================================");
        }

        public string enterPaymentMenthod()
        {
            ConsoleKeyInfo key;
            string report="";
            string PaymentMethod = "";
            do
            {
                Console.Clear();
                Logo();
                Console.Write(@"
                    =============================================================================================================================
                    |                                                                                                                           |
                    |                                              --- Enter Payment Method---                                                  |
                    |                                                                                                                           |
                    =============================================================================================================================
                    |                                                                                                                           |
                    |                               Payment method: [{0}]                                                   |
                    |                                                                                                                           |
                    =============================================================================================================================
                    {1}", addSpaceToStr(limitChar(PaymentMethod, 20, 20)+"_", 23), report);
                key = Console.ReadKey(true);
                if (key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z || key.Key == ConsoleKey.Spacebar)
                {
                    PaymentMethod += key.KeyChar;
                }else if (key.Key == ConsoleKey.Backspace && PaymentMethod.Length > 0)
                {
                    PaymentMethod = PaymentMethod.Substring(0, PaymentMethod.Length-1);
                }
                else
                {
                    report = "[!] Can only enter word";
                }
            } while (key.Key != ConsoleKey.Enter);
            return PaymentMethod;
        }

        public string limitChar(string str, int limitMax, int length)
        {
            if (str.Length > limitMax)
            {
                return str.Substring(str.Length - length, length);
            }
            return str;
        }

        public Staff loginBasic(List<Staff> listStaff)
        {
            Staff staff = new Staff();
            ConsoleKeyInfo key;
            bool check = true;
            do{
                Console.Clear();
                Logo();
                Console.Write(@"
                    |                                                                                                                           |                            
                    |                                                      ---Login---                                                          |
                    |                                                                                                                           |
                    =============================================================================================================================
                        ");
                Console.Write(@"
                        Username : ");
                if (staff.UserName == "")
                {
                    do
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Tab)
                        {
                            staff.UserName = "";
                            staff.Password = "";
                            return staff;
                        }else if (key.Key == ConsoleKey.Backspace)
                        {
                            staff.UserName = "";
                        }else if ((key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z) || (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9))
                        {
                            staff.UserName += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                    } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace);
                }else
                {
                    Console.Write(staff.UserName);
                }
                if(staff.Password == "" && staff.UserName != "")
                {
                    Console.Write(@"
                        Password : ");
                    do
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Tab)
                        {
                            staff.UserName = "";
                            staff.Password = "";
                            return staff;
                        }
                        else if (key.Key == ConsoleKey.Backspace)
                        {
                            staff.Password = "";
                        }else if (staff.Password == "" && key.Key == ConsoleKey.Backspace)
                        {
                            staff.UserName = "";
                        }
                        else if ((key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z) || (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9))
                        {
                            
                            staff.Password += key.KeyChar;
                            Console.Write("*");
                        }
                    } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace);
//<><><><><><><><><><><><><><><><><><><><><><><><>
                    // byte array representation of that string
                    byte[] encodedPassword = new UTF8Encoding().GetBytes(staff.Password);

                    // need MD5 to calculate the hash
                    byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

                    // string representation (similar to UNIX format)
                    string encoded = BitConverter.ToString(hash)
                    // without dashes
                    .Replace("-", string.Empty)
                    // make lowercase
                    .ToLower();

                    // encoded contains the hash you want
//<><><><><><><><><><><><><><><><><><><><><><><><>
                    if (staff.UserName != "" && staff.Password != "")
                    {
                        foreach (Staff item in listStaff)
                        {
                            if(item.UserName == staff.UserName && item.Password == encoded)
                            {
                                check = false;
                                staff = item;
                                break;
                            }
                        }
                        if (check == true)
                        {
                            Console.Write(@"
                        [!] Username or password invalid.");
                        }

                    }
                }
            }while(check != false);
            
            return staff;
        }

        


    }
}
    
