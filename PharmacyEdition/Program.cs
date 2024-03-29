﻿using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Services;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;
//// See https://aka.ms/new-console-template for more information

//using PharmacyEdition.Data.IRepositories;
//using PharmacyEdition.Data.Repositories;
//using PharmacyEdition.Domain.Enums;
//using PharmacyEdition.Models;
//using PharmacyEdition.Service.DTOs;
//using PharmacyEdition.Service.Interfaces;
//using PharmacyEdition.Service.Services;

//GenericRepository<Medicine> medicineRepository = new GenericRepository<Medicine>();

//IMedicineService medicineService = new MedicineService();
//IOrderService orderService = new OrderService();

//MedicineCreationDto medicineDto = new MedicineCreationDto()
//{
//    Name = "Parestamol",
//    Count = 10,
//    Description = "Bosh",
//    Price = 1000
//};

//var paymentDto = new PaymentCreationDto()
//{
//    IsPaid = true,
//    OrderId = 2,
//    Type = PaymentType.Humo
//};

//var medicine = await medicineService.GetByIdAsync(1);
//var medicine2 = await medicineService.GetByIdAsync(2);

//OrderCreationDto orderCreationDto = new OrderCreationDto()
//{
//    Medicines = new List<Medicine>() { medicine.Value, medicine2.Value },
//    Payment = paymentDto
//};

////await medicineService.CreateAsync(medicineDto);
//await orderService.CreateAsync(orderCreationDto);



//IUserRepository ur = new UserRepository();

//await ur.InsertAsync(new User()
//{
//    Age = 18,
//    FirstName = "Muzaffar",
//    LastName = "Nurillayev",
//    Phone = "234",
//    Password = "password"
//});

//var res = await ur.SelectAsync(u=> true);

//Console.WriteLine(res.FirstName);

var ur = new UserService();

//var res = await ur.AddAsync(new UserCreationDto()
//{
//    Age = 20,
//    FirstName = "Abdulloh",
//    LastName = "Axmadjonov",
//    Phone = "987",
//    Password = "notpassword"
//});

//var res2 = await ur.GetByIdAsync(res.Value.Id);
var res2 = await ur.LoginAsync(phone: "987", password: "notpassword");
Console.WriteLine(res2.Value?.FirstName);