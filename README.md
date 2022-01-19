# Pluggable Persistence Layer

>A simple yet handy framework-agnostic .NET persistence wrapper.

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
                    .Build();
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
                    .Build();
```
And then the driver will understand that there's a collection named `artists` that contains entities of type `Artist` in your database. 


Speaking of the devil, you would interact with the persistence layer through the driver using a good old CRUD:

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

Yes, there are transactions involved (for those providers that support them), and of course you may revert the changes made to the database when something goes wrong:

```csharp
...
try {
    driver.Insert<Artist>(new Artist {
        ...
    });
    throw new InvalidOperationException("Oopsie");
    driver.SaveChanges();
}
catch {
    driver.DiscardChanges();
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

Pretty straight-forward, isn't it?
