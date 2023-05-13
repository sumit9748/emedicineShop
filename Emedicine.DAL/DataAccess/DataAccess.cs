using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.DataAccess
{
    public class DataAccess : IDataAccess
    {
        public readonly MedicineDbContext md;
        public DataAccess(MedicineDbContext _md)
        {
            md = _md;
            user=new UserRepo(md);
            medicine = new MedicineRepo(md);
            medicalShop = new MedicalShopRepo(md);
            order=new OrderRepo(md);
            medicalShop=new MedicalShopRepo(md);
            orderItem = new OrderItemRepo(md);
            cart=new CartRepo(md);
        }
        public IUser user { get;private set; }
        public IMedicine medicine { get; private set; }
        public IMedicalShop medicalShop { get; private set; }
        public IOrder order { get; private set; }   
        public IMedicalShopItem medicalShopItem { get; private set; }
        public IOrderItem orderItem { get; private set; }
        public ICart cart { get; private set;}
        public void save()
        {
            md.SaveChangesAsync();
        }

    }
    public class UserRepo : Repo<User>, IUser
    {
        public readonly MedicineDbContext md;
        public DbSet<User> UserDbSet { get; set; }
        public UserRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
            UserDbSet = md.Set<User>();
        }
        
    }
    public class MedicineRepo : Repo<Medicine>, IMedicine
    {
        public readonly MedicineDbContext md;
        public MedicineRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class MedicalShopRepo : Repo<Medicalshop>, IMedicalShop
    {
        public readonly MedicineDbContext md;
        public MedicalShopRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class OrderRepo : Repo<Order>, IOrder
    {
        public readonly MedicineDbContext md;
        public OrderRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class OrderItemRepo : Repo<OrderItem>, IOrderItem
    {
        public readonly MedicineDbContext md;
        public OrderItemRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class MedicalShopItemRepo : Repo<MedicalShopItem>, IMedicalShopItem
    {
        public readonly MedicineDbContext md;
        public MedicalShopItemRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class CartRepo : Repo<Cart>, ICart
    {
        public readonly MedicineDbContext md;
        public CartRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
}
