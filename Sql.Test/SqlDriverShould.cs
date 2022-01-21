using System;
using System.Linq;
using FluentAssertions;
using PluggablePersistenceLayer.Core.Drivers;
using Sql.Test.Models;
using Xunit;

namespace Sql.Test {
    public abstract class SqlDriverShould : IDisposable {

        protected abstract IDriver Driver { get; }

        protected SqlDriverShould() {
            Driver.Insert(new User {
                Nickname = "dummy",
            });
            Driver.SaveChanges();
        }
        
        [Fact]
        public void GetAllUsers() {
            Driver.GetAll<User>().Should().NotBeEmpty();
        }
        
        [Fact]
        public void CheckThatOneUserExistsByFilter() {
            Driver.Exists<User>(u => u.Nickname == "dummy").Should().BeTrue();
        }
        
        [Fact]
        public void GetOneUser() {
            Driver.GetAll<User>(u => u.Nickname == "dummy")
                .SingleOrDefault()
                .Should().NotBeNull();
        }
        
        protected static User CreateTempUser() {
            return new User {
                Nickname = "temp",
            };
        }
        
        private static User CreateInvalidUser() {
            return new User {
                Nickname = "ahdsasdhaidajjasjdahfjhakfh",
            };
        }
        
        [Fact]
        public void InsertOneUser() {
            var user = Driver.Insert(CreateTempUser());
            user.Id.Should().NotBeEmpty();
        }
        
        [Fact]
        public void DeleteOneUser() {
            var user = Driver.Insert(CreateTempUser());
            user.Id.Should().NotBeEmpty();
            Driver.SaveChanges();
            Driver.Remove(user).SaveChanges();
            Driver.Exists(user).Should().BeFalse();
        }
        
        [Fact]
        public void UpdateOneUser() {
            var user = Driver.Insert(CreateTempUser());
            user.Id.Should().NotBeEmpty();
            Driver.SaveChanges();
            user.Nickname = "temp2";
            var updatedUser = Driver.Update(user);
            updatedUser.Nickname.Should().Be(user.Nickname);
            Driver.Remove(updatedUser).SaveChanges();
        }
        
        [Fact]
        public void WorkWithTransactions() {
            Driver.BeginTransaction();
            var user = Driver.Insert(CreateInvalidUser());
            user.Id.Should().NotBeEmpty();
            var a = () => {
                Driver.SaveChanges();
                Driver.CommitTransaction();
            };
            a.Should().Throw<Exception>();
            Driver.Exists(user).Should().BeFalse();
            Driver.RollbackTransaction();
            
            Driver.BeginTransaction();
            user = CreateTempUser();
            Driver.Insert(user);
            Driver.SaveChanges();
            Driver.CommitTransaction();
            Driver.Exists(user).Should().BeTrue();
        }

        public void Dispose() {
            Driver.RemoveAll<User>();
            Driver.RemoveAll<Booking>();
            Driver.SaveChanges();
        }
    }
}