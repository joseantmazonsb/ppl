# Pluggable Persistence Layer

>A handy framework-agnostic .NET persistence wrapper.

PPL allows you to quickly set up the persistence layer of your choosing, as simple as this:

```csharp
using PluggablePersistenceLayer.SqlServer;
...
var connectionString = "...";
var driver = new SqlServerDriverBuilder(connectionString)
                    .WithDataset<Artist>()
                    .WithDataset<Album>()
                    .WithDataset<Song>()
                    .WithDataset<Playlist>()
                    .Build(withDatabaseCreated: true);
```

But, **what are those datasets**?

Well, the datasets you register would be the tables/collections stored in your database. By default, the framework assumes the dataset is named after its type, so the previous example assumes your database has (or will have) a collection named `Artist`, a collection named `Album`, and so on. However, things may be different in real life, so you can also specify the name of the dataset along with its type this way:

```csharp
using PluggablePersistenceLayer.SqlServer;
...
var connectionString = "...";
var driver = new SqlServerDriverBuilder(connectionString)
                    .WithDataset<Artist>("artists")
                    .WithDataset<Album>()
                    .WithDataset<Song>()
                    .WithDataset<Playlist>()
                    .Build(withDatabaseCreated: true);
```
Then, the driver will understand that there's a collection named `artists` that contains entities of type `Artist` in your database. Note that we're setting the argument `withDatabaseCreated` to `true`. This will ensure the database is created. By default, the drivers will not create the database if it does not exist, but they implement two methods to create and delete databases:

```csharp
driver.EnsureDatabaseCreated();
...
driver.EnsureDatabaseDeleted();
```

Of course, you would interact with the persistence layer using a good old CRUD:

```csharp
...
driver.Exists<Artist>(a => a.Name == "alice"))
    .Should()
    .BeFalse();
var artist = driver.Insert(new Artist {
    Name = "alice",
    ...
});
driver.SaveChanges();
driver.GetAll<Artist>(a => a.Name == "alice")
    .Single()
    .Should()
    .Be(user);
artist.Name = "bob";
driver.Update(artist)
    .Should
    .Be(artist);
driver.SaveChanges();
driver.GetAll<Artist>(a => a.Name == artist.Name)
    .Should()
    .NotBeEmpty();
driver.Remove(artist)
    .Should()
    .Be(artist);
driver.SaveChanges();
driver.GetAll<Artist>(a => a.Name == artist.Name)
    .Should()
    .BeEmpty();

Action invalid = () => driver.GetAll<Songwriter>();
invalid.Should().Throw<InvalidOperationException>(); // Class Songwriter has no registered dataset
```

And yes, you can also use transactions yourself (for those providers that support them) the same way you've been doing with **EntityFramework**:

```csharp
...
driver.BeginTransaction();
try {
    driver.Insert<Artist>(new Artist {
        Name = "martha"
    });
    ...
    driver.SaveChanges();
    driver.CommitTransaction();
}
catch {
    driver.RollbackTransaction();
    driver.GetAll<Artist>(a => a.Name == "martha")
        .Should()
        .BeEmpty();
}
```

As you would have imagined, PPL supports multiple database providers, such as MySql, Postgres or MongoDb; you only need to install the right Nuget package:

```csharp
using PluggablePersistenceLayer.MySql;
...
var connectionString = "...";
var driver = new MySqlDriverBuilder(connectionString)
                    .WithDataset<Artist>()
                    .WithDataset<Album>()
                    .WithDataset<Song>()
                    .WithDataset<Playlist>()
                    .Build();
```

```csharp
using PluggablePersistenceLayer.MongoDb;
...
var connectionString = "...";
var driver = new MongoDbDriverBuilder(connectionString)
                    .WithDataset<Artist>()
                    .WithDataset<Album>()
                    .WithDataset<Song>()
                    .WithDataset<Playlist>()
                    .Build();
```

Pretty straight-forward, isn't it? There's more, but you got the idea.