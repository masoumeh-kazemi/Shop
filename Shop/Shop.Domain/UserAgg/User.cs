﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System.Net;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{ 
    private User()
    {

    }
    public User(string name, string family, string phoneNumber, string email, string password,
        Gender gender, IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
        AvatarName = "avatar.png";
        IsActive = true;
        Roles = new();
        Wallets = new();
        Addresses = new();
        Tokens = new();
    }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string AvatarName { get; private set; }
    public bool IsActive { get; set; }
    public Gender Gender { get; private set; }
    public List<Wallet> Wallets { get; }
    public List<UserRole> Roles { get; }
    public List<UserAddress> Addresses { get;}
    public List<UserToken> Tokens { get; }

    public void Edit(string name, string family, string phoneNumber, string email,
        Gender gender, IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void SetAvatar(string avatarName)
    {
        AvatarName = avatarName;
    }

    public static User Register(string phoneNumber, string password, IUserDomainService userDomainService)
    {
        return new User("", "", phoneNumber, null, password, Gender.None, userDomainService);
    }


    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void DeleteAddress(long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        Addresses.Remove(oldAddress);
    }

    public void EditAddress(UserAddress address, long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        oldAddress.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress, address.PhoneNumber, address.Name
            , address.Family, address.NationalCode);
    }

    public void SetActiveAddress(long addressId)
    {
        var currentAddress = Addresses.FirstOrDefault(f=>f.Id == addressId);
        if (currentAddress == null)
            throw new NullOrEmptyDomainDataException("Address not found");

        foreach (var address in Addresses)
        {
            address.SetDeActive();
        }

        currentAddress.SetActive();
    }
    public void ChangePassword(string newPassword)
    {
        NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));
        Password = newPassword;
    }
    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    public void SetRoles(List<UserRole> roles)
    {
        roles.ForEach(f => f.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void AddToken(string hasJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
        if (activeTokenCount == 3)
            throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه همزمان وجود ندارد");

        var token = new UserToken(hasJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        token.UserId = Id;
        Tokens.Add(token);
    }

    public void RefreshToken(long TokenId)
    {
        var token = Tokens.FirstOrDefault(f => f.Id == TokenId);
        if (token == null)
            throw new InvalidDomainDataException("Invalid TokenId");

        Tokens.Remove(token);
    }

    public void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        //NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

        if(!string.IsNullOrWhiteSpace(email)) 
            if (email.IsValidEmail() == false)
             throw new InvalidDomainDataException("ایمیل نامعتبر است");

        if (phoneNumber != PhoneNumber)
            if (userDomainService.PhoneNumberIsExist(phoneNumber))
                throw new InvalidDomainDataException("شماره موبایل تکراری است");

        if (email != Email)
            if (userDomainService.IsEmailExist(email))
                throw new InvalidDomainDataException("ایمیل تکراری است");

    }

}