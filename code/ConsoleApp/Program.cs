﻿using BL;
using Persistence;
using DAL;
using CS;
using MySqlConnector;
using System;
using System.Text;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Ultilities CS = new Ultilities();
        Clothes clothes = new Clothes();
        Staff staff = new Staff(); 
        Customer customer = new Customer();
        Order order = new Order();

        ClothesDAL clDAL = new ClothesDAL();
        

        OrderBL oBL = new OrderBL();
        OrderDetailsBL ordDtlsBL = new OrderDetailsBL();
        CustomerBL cBL = new CustomerBL();
        ClothesBL clBL = new ClothesBL();
        StaffBL stBL = new StaffBL();
        ColorBL colBL = new ColorBL();
        SizeBL sBL = new SizeBL();
        SizeColorBL szclBL = new SizeColorBL();
        CategoryBL cgrBL = new CategoryBL();
        OrderDetails orderDetails;

        List<Clothes> ListClothes = new List<Clothes>();
        List<Color> ListColor = new List<Color>();
        List<Customer> ListCustomer = new List<Customer>();
        List<Size_color> ListSizeColor = new List<Size_color>();
        List<Size> ListSize = new List<Size>();
        List<Staff> listStaff = new List<Staff>();
        List<Categories> ListCategories = new List<Categories>();
        List<rowPageSpl> ListRowPage = new List<rowPageSpl>();
        List<Order> ListOrder = new List<Order>();
        List<OrderDetails> ListOrderDetail = new List<OrderDetails>();
        
        // string[] cashierMenu = { "Create Order.", "Confirm Order.", "Log Out." };
        string[] MainMenu = { "Create New Order.", "Log Out." };
        // string[] loginMenu = { "Login.", "Exit." };
        string[] createOrderMenu = {"Show All.", "Show Clothes List By Category.", "Show Order", "Back To Menu."};
        // string[] confirmOrderMenu = { "Show Order Detail.", "Confirm Order", "Cancel Order"};
        string[] stepMenu = {"New order", "Choose Clothes", "Enter quantity", "Show order", "Update order", "Print invoice"};
        StaffBL uBL = new StaffBL();

        int category_ID = 0;
        string text, checkStr="";
        bool checkTF = true, checkLogin = true;
        int status = 0, statusOrder = 0;
        string report = "";
        string access = "";
        string Login = "TITLE";
        int orderChoice =0;
        do
        {
            listStaff = stBL.GetAllAccount();
            int checkPass =0;
            Console.Clear();
            if (staff.Password == "" && staff.UserName == "" && Login == "TITLE" && orderChoice != 2)
            {
                Login = "BASIC";
            }else if (staff.Password == "" && staff.UserName == "" && Login == "BASIC" && orderChoice != 2)
            {
                Login = "TITLE";
            }else
            {
                foreach (Staff item in listStaff)
                {
                    if (item.UserName == staff.UserName && item.Password == staff.Password)
                    {
                        checkPass = 1;
                        break;
                    }
                }
            }
            if (Login == "TITLE" && checkPass == 0)
            {
                staff = CS.LoginMenu(listStaff);
            }else if (Login == "BASIC" && checkPass == 0)
            {
                staff = CS.loginBasic(listStaff);
                
            }else if (checkPass == 1)
            {
                string[] unprocessedAction = { "Change Status To Processing...", "Back To Previous Menu" };
                string[] processingAction = { "Change Status To Completed", "Back To Previous Menu" };
                string infoStaff = "[Cashier: @" + staff.UserName + " | " + staff.NameStaff + " ]";
                bool active = true;
                
                while(active)
                {
                    ListClothes = clBL.GetAllProduct();
                    ListColor = colBL.GetListColor();
                    ListSize = sBL.GetListSize();
                    ListSizeColor = szclBL.GetSize_Colors();
                    ListCategories = cgrBL.GetCategories();
                    Console.Clear();
                    orderChoice = CS.MenuHandle(@"
                    |                                                                                                                           |
                    |                                                   ---- Main Menu ----                                                     |", MainMenu, infoStaff, stepMenu, 0);
                    // if (orderChoice == 2)
                    // {
                    //     break;
                    // }
                    // else if(orderChoice == -1)
                    // {
                    //     return;
                    // }
                    switch (orderChoice)
                    {
                        case 1:
                            order = oBL.createNewOrder(staff.ID, staff.NameStaff, status);
                                int filterChoice = 1;
                                while(filterChoice != 0)
                                {
                                    if (filterChoice != -2 && filterChoice != 4)
                                    {
                                        Console.Clear();
                                        filterChoice = CS.MenuHandle(@"
                    |                                                                                                                           |
                    |                                            ---- Create Order Menu ----                                                    |", createOrderMenu, infoStaff, stepMenu, 1);
                                        if (filterChoice == -1)
                                        {
                                        }
                                    }else if (filterChoice == -2)
                                    {
                                        filterChoice = 3;
                                        access = "ACCESS";
                                    }
                                    switch (filterChoice)
                                    {
                                        case 1:
                                            checkTF = true;
                                            text = @"
                    [?] Are you sure you want to show all the clothes?";
                                            checkStr = CS.pressEnterTab(@"
                    |                                                                                                                           |
                    |                                            ---- Create Order Menu ----                                                    |", createOrderMenu, filterChoice, infoStaff, text, stepMenu, 1);
                                            if (checkStr == "ESCAPE")
                                            {
                                                return;
                                            }
                                            else if (checkStr == "TAB")
                                            {
                                                break;
                                            }
                                            string nameClothes = "";
                                            while(checkTF)
                                            {
                                                category_ID = 0;
                                                string title = @"
                    |                                                                                                                           |
                    |                                              ---- Clothes List ----                                                       |";
                                                nameClothes = CS.PageSplit(ListRowPage, ListClothes, ListSizeColor, ListSize, ListColor, ListCategories, title, infoStaff, category_ID, stepMenu, 2);
                                                string quantity;
                                                if (nameClothes != "" && nameClothes != "C" && nameClothes != "TAB")
                                                {
                                                    int ID = CS.choiceClothesBySzcl(nameClothes, ListClothes, ListSizeColor, ListSize, ListColor, infoStaff, stepMenu, 2);
                                                    if(ID != 0 && ID != -1)
                                                    {
                                                        quantity = CS.showInfoClothes(ID, ListCategories, ListClothes, ListSizeColor, ListSize, ListColor, stepMenu, 3);
                                                        orderDetails = new OrderDetails();
                                                        if(quantity == "ESCAPE")
                                                        {
                                                            return;
                                                        }else if (quantity == "TAB")
                                                        {
                                                        }else
                                                        {
                                                            statusOrder = 1;
                                                            int quantityOrder = Convert.ToInt32(quantity);
                                                            int Unit_price=0;
                                                            foreach (Clothes item in ListClothes)
                                                            {
                                                                if(item.Name == nameClothes)
                                                                {
                                                                    Unit_price=item.Unit_price;
                                                                    break;
                                                                }
                                                            }
                                                            foreach (Size_color item_SizeColor in ListSizeColor)
                                                            {
                                                                if (item_SizeColor.clothes_ID == ID)
                                                                {
                                                                    item_SizeColor.Quantity -= quantityOrder;
                                                                    break;
                                                                }
                                                            }
                                                            order.TotalPrice += Unit_price*quantityOrder;
                                                            int checkQuantity=0;
                                                            foreach (OrderDetails item in ListOrderDetail)
                                                            {
                                                                if (item.ClothesID == ID)
                                                                {
                                                                    item.Quantity += quantityOrder;
                                                                    checkQuantity =1;
                                                                    break;
                                                                }
                                                            }
                                                            if (checkQuantity==0)
                                                            {
                                                                orderDetails  = ordDtlsBL.addClothesToOrder(order.OrderID, ID, Unit_price, quantityOrder);
                                                                ListOrderDetail.Add(orderDetails);
                                                            }
                                                        }
                                                    }else if (ID == -1)
                                                    {
                                                        filterChoice = -2;
                                                        break;
                                                    }
                                                }else if(nameClothes == "TAB")
                                                {
                                                    checkTF = false;
                                                    break;
                                                }else if (nameClothes == "C")
                                                {
                                                    filterChoice = -2;
                                                    break;
                                                }
                                            }
                                            break;
                                        case 2:
                                            text = @"
                    [?] Are you sure you want to show clothes list by category?";
                                            checkStr = CS.pressEnterTab(@"
                    |                                                                                                                           |
                    |                                            ---- Create Order Menu ----                                                    |", createOrderMenu, filterChoice, infoStaff, text, stepMenu, 1);
                                            if (checkStr == "ESCAPE")
                                            {
                                                return;
                                            }else if (checkStr == "TAB")
                                            {
                                                break;
                                            }
                                            checkTF=true;
                                            category_ID = CS.choiceCategory(ListCategories, infoStaff, stepMenu, 2);
                                            if (category_ID == -1)
                                            {
                                                return;
                                            }else if (category_ID == 0)
                                            {
                                                break;
                                            }
                                            // foreach (Categories item in ListCategories)
                                            // {
                                            //     if (item.ID == CategoryID)
                                            //     {
                                            //         conditionStr = item.Category_name;
                                            //         break;
                                            //     }
                                            // }
                                            nameClothes = "";
                                            while (checkTF)
                                            {
                                                string title = @"
                    |                                                                                                                           |
                    |                                          ---- Clothes List By Category ----                                               |";
                                                nameClothes = CS.PageSplit(ListRowPage, ListClothes, ListSizeColor, ListSize, ListColor, ListCategories, title, infoStaff, category_ID, stepMenu, 2);
                                                if(nameClothes == "TAB")
                                                {
                                                    break;
                                                }if (nameClothes == "C")
                                                {
                                                    filterChoice = -2;
                                                    break;
                                                }
                                                string quantity;
                                                bool checkList = true;
                                                while (checkList && nameClothes != "")
                                                {
                                                    if (nameClothes != "")
                                                    {
                                                        int ID = CS.choiceClothesBySzcl(nameClothes, ListClothes, ListSizeColor, ListSize, ListColor, infoStaff, stepMenu, 2);
                                                        if(ID == 0) break;
                                                        else if(ID == -1){
                                                             filterChoice = -2;
                                                             checkTF = false;
                                                             break;
                                                        }else
                                                        {
                                                            quantity = CS.showInfoClothes(ID, ListCategories, ListClothes, ListSizeColor, ListSize, ListColor, stepMenu, 3);
                                                            checkList = false;
                                                            orderDetails = new OrderDetails();
                                                            if (quantity == "Create Order Menu")
                                                            {
                                                                checkList = false;
                                                            }else if (quantity == "TAB")
                                                            {
                                                                
                                                                break;
                                                            }else
                                                            { 
                                                                statusOrder = 1;
                                                                int quantityOrder = Convert.ToInt32(quantity);
                                                                int Unit_price=0;
                                                                foreach (Clothes item in ListClothes)
                                                                {
                                                                    if(item.ID == ID)
                                                                    {
                                                                        Unit_price=item.Unit_price;
                                                                        break;
                                                                    }
                                                                }
                                                                foreach (Size_color item_SizeColor in ListSizeColor)
                                                                {
                                                                    if (item_SizeColor.clothes_ID == ID)
                                                                    {
                                                                        item_SizeColor.Quantity -= quantityOrder;
                                                                        break;
                                                                    }
                                                                }
                                                                order.TotalPrice += Unit_price*quantityOrder;
                                                                int checkQuantity=0;
                                                                foreach (OrderDetails item in ListOrderDetail)
                                                                {
                                                                    if (item.ClothesID == ID)
                                                                    {
                                                                        item.Quantity += quantityOrder;
                                                                        checkQuantity =1;
                                                                        break;
                                                                    }
                                                                }
                                                                if (checkQuantity==0)
                                                                {
                                                                    orderDetails  = ordDtlsBL.addClothesToOrder(order.OrderID, ID, Unit_price, quantityOrder);
                                                                    ListOrderDetail.Add(orderDetails);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 3:
                                        statusOrder =0;
                                        foreach (OrderDetails item in ListOrderDetail)
                                        {
                                            if (item.Quantity > 0)
                                            {
                                                statusOrder = 1;
                                                break;
                                            }
                                        }
                                            List<OrderDetails> checkOrderdetail = new List<OrderDetails>();
                                            if (access != "ACCESS")
                                            {
                                                text = @"
                    [?] Show order?";
                                                checkStr = CS.pressEnterTab(@"
                    |                                                                                                                           |
                    |                                           ---- Create Order Menu ----                                                     |", createOrderMenu, filterChoice, infoStaff, text, stepMenu, 1);
                                                
                                            }
                                            if ((statusOrder == 0 && checkStr == "ENTER") || (statusOrder == 0 && access == "ACCESS"))
                                            {
                                                do
                                                {
                                                    text = @"
                    [!] There are no clothes in the order.
                    {Enter} to continue.";
                                                    checkStr = CS.pressEnterTab(@"
                    |                                                                                                                           |
                    |                                           ---- Create Order Menu ----                                                     |", createOrderMenu, filterChoice, infoStaff, text, stepMenu, 1);
                                                } while (checkStr != "ENTER");
                                                break;
                                            }
                                            else if ((statusOrder == 1 && checkStr != "TAB") || (statusOrder == 1 && access == "ACCESS"))
                                            {
                                                order.status = 0;
                                                checkOrderdetail = CS.ShowOrderDetails(order, ListOrderDetail, ListClothes, ListSizeColor, ListSize, ListColor, ListCategories, customer.Name, customer.PhoneNumber, staff.NameStaff, order.status, stepMenu, 4);
                                                if (checkOrderdetail == null)
                                                {
                                                    break;
                                                }else if (checkOrderdetail.Count() == 0)
                                                {
                                                    ListOrderDetail = checkOrderdetail;
                                                    filterChoice = 4;
                                                }
                                                else
                                                {
                                                    statusOrder =0;
                                                    foreach (OrderDetails item in ListOrderDetail)
                                                    {
                                                        if (item.Quantity > 0)
                                                        {
                                                            statusOrder = 1;
                                                            break;
                                                        }
                                                    }
                                                    if (statusOrder == 0)
                                                    {
                                                        do
                                                        {
                                                            text = @"
                    [!] There are no clothes in the order.
                    {Enter} to continue.";
                                                            checkStr = CS.pressEnterTab(@"
                    |                                                                                                                           |
                    |                                           ---- Create Order Menu ----                                                     |", createOrderMenu, filterChoice, infoStaff, text, stepMenu, 1);
                                                        } while (checkStr != "ENTER");
                                                        break;
                                                    }else
                                                    {
                                                        // string phoneNum;
                                                        ListCustomer = cBL.GetAllCustomer();
                                                            status = 0;
                                                            statusOrder = 0;
                                                            Console.Clear();
                                                            customer = CS.enterPhoneCustomer(ListCustomer, infoStaff, stepMenu, 4);
                                                            if (customer.Name == "")
                                                            {
                                                                report = @"
                    [!] The customer is not in the database yet.
                    Please enter customer name!";
                                                                customer.Name = CS.newCustomer(infoStaff, customer.PhoneNumber, report, stepMenu, 4);
                                                                if (customer.Name != "TAB")
                                                                {
                                                                    customer = cBL.UpCustomerToDB(customer.PhoneNumber, customer.Name, ListCustomer);
                                                                    ListCustomer = cBL.GetAllCustomer();
                                                                    foreach (Customer item in ListCustomer)
                                                                    {
                                                                        customer = item;
                                                                    }
                                                                }else if (customer.Name == "TAB")
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                            else if (customer.Name == "TAB")
                                                            {
                                                                break;
                                                            }
                                                            
                                                            checkOrderdetail = new List<OrderDetails>();
                                                            order.status = 1;
                                                            checkOrderdetail = CS.ShowOrderDetails(order, ListOrderDetail, ListClothes, ListSizeColor, ListSize, ListColor, ListCategories, customer.Name, customer.PhoneNumber, staff.NameStaff, order.status, stepMenu, 6);
                                                             
                                                        if (checkOrderdetail.Count() == 0)
                                                        {
                                                            ListOrderDetail = checkOrderdetail;
                                                            filterChoice = 4;
                                                        }
                                                        else
                                                        {
                                                            // List<Size_color> temp = szclBL.GetSize_Colors();
                                                            // int tempQuantity = 0;
                                                            // foreach (OrderDetails item_orderdetail in ListOrderDetail)
                                                            // {
                                                            //     foreach (Size_color item_szcl in ListSizeColor)
                                                            //     {
                                                                    
                                                            //         if (item_orderdetail.ClothesID == item_szcl.clothes_ID)
                                                            //         {
                                                            //             foreach (Size_color item_temp in temp)
                                                            //             {
                                                            //                 if (item_temp.clothes_ID == item_szcl.clothes_ID)
                                                            //                 {
                                                            //                     tempQuantity = item_temp.Quantity;
                                                            //                     break;
                                                            //                 }
                                                            //             }
                                                            //             item_szcl.Quantity = tempQuantity - item_orderdetail.Quantity;
                                                            //             break;
                                                            //         }
                                                            //     }
                                                            // }
                                                            order.CustomerID = customer.ID;
                                                            order.customerPhone = customer.PhoneNumber;
                                                            string infoCustomer = "[Customer : <phone> " + customer.PhoneNumber + " | <name> " + customer.Name + " ]";
                                                            order.PaymentMethod = CS.enterPaymentMenthod();
                                                            if (CS.ShowOrderDetails(order, ListOrderDetail, ListClothes, ListSizeColor, ListSize, ListColor, ListCategories, customer.Name, customer.PhoneNumber, staff.NameStaff, order.status, stepMenu, 6).Count == 0)
                                                            {
                                                                filterChoice = 4;
                                                                break;
                                                            }
                                                            oBL.InsertOrder(order, ListOrderDetail);
                                                            oBL.updateDataMysql(ListSizeColor, ListOrderDetail);
                                                            CS.ConfirmOrder();
                                                            order = new Order();
                                                            ListOrder = new List<Order>();
                                                            ListOrderDetail = new List<OrderDetails>();
                                                            customer = new Customer();
                                                            filterChoice = 4;
                                                        }
                                                    }
                                                }
                                            }else if (checkStr == "ESCAPE")
                                            {
                                                return;
                                            }else if (checkStr == "TAB")
                                            {
                                                break;
                                            }
                                            break;
                                        case 4:
                                            if (ListOrderDetail.Count() == 0)
                                            {
                                                filterChoice = 0;
                                                break;
                                            }
                                            text = @"
                    [?] Back to Main Menu?";
                                            checkStr = CS.pressEnterTab(@"
                    |                                                                                                                           |
                    |                                           ---- Create Order Menu ----                                                     |", createOrderMenu, filterChoice, infoStaff, text, stepMenu, 1);
                                            if (checkStr == "ENTER")
                                            {
                                                filterChoice = 0;
                                                order = new Order();
                                                ListOrder = new List<Order>();
                                                ListOrderDetail = new List<OrderDetails>();
                                                customer = new Customer();
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            break;
                        case 2:
                            staff = new Staff();
                            active = false;
                            break;
                        default:
                            break;
                    }
                }
            }

        } while (checkLogin);
    }
}
