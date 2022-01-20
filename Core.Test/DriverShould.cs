using System;
using System.Linq;
using Core.Test.Models;
using FluentAssertions;
using PluggablePersistenceLayer.Core.Drivers;
using Xunit;

namespace Core.Test {
    public abstract class DriverShould : IDisposable {
        
        protected abstract IDriver Driver { get; }

        protected DriverShould() {
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
            Driver.Remove(user);
            Driver.SaveChanges();
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
            Driver.Remove(updatedUser);
            Driver.SaveChanges();
        }
        
        [Fact]
        public void DiscardAllChanges() {
            try {
                Driver.BeginTransaction();
            }
            catch {
                return;
            }
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