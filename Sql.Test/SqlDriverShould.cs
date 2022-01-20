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
                JoinDate = DateTime.Now,
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
        
        private static User CreateTempUser() {
            return new User {
                JoinDate = DateTime.Now,
                Nickname = "temp"
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
        public void DiscardChanges() {
            Driver.BeginTransaction();
            var user = Driver.Insert(CreateTempUser());
            user.Id.Should().NotBeEmpty();
            Driver.SaveChanges();
            Driver.Exists(user).Should().BeTrue();
            Driver.RollbackTransaction();
            Driver.Exists(user).Should().BeFalse();
        }

        public void Dispose() {
            Driver.RemoveAll<User>();
            Driver.RemoveAll<Booking>();
            Driver.SaveChanges();
        }
    }
}