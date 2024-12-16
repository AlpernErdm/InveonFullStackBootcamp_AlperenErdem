# Library Management System

Bu proje, kitapların yönetimini sağlayan basit bir kütüphane yönetim sistemidir. Proje ASP.NET Core MVC kullanılarak geliştirilmiştir ve kullanıcı kimlik doğrulama sistemi ve CRUD işlemleri içerir.

## Özellikler

- **Kitap Listeleme**: Sistemde mevcut tüm kitapların bir tabloda listelendiği sayfa.
- **Kitap Detayları**: Belirli bir kitabın ayrıntılarını gösteren sayfa.
- **Kitap Güncelleme**: Kitapların bilgilerini düzenleme işlemi.
- **Kitap Silme**: Kitapların sistemden silinmesi işlemi.
- **Kitap Ekleme**: Yeni kitapların eklenmesi için form.
- **Kitap Arama**: Kitapların başlık, yazar veya ISBN bilgisine göre aranmasını sağlayan işlev.
- **Kullanıcı Yönetimi**: Kullanıcı ekleme, görüntüleme, güncelleme ve silme.
- **Rol Yönetimi**: Rol ekleme, görüntüleme, güncelleme ve silme.
- **Kimlik Doğrulama ve Yetkilendirme**: Kullanıcıların kayıt olması, giriş yapması ve çıkış yapması.

## Gereksinimler

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (veya bir başka ilişkisel veritabanı)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) veya daha yeni bir sürüm

## Kurulum

### 1. Projeyi Klonlama

Öncelikle projeyi yerel makinenize klonlayın:

git clone https://github.com/AlpernErdm/InveonFullStackBootcamp_AlperenErdem/tree/Week2Work/WebAutomation
cd library-management-system
### 2. Gerekli Paketlerin Yüklenmesi

Projeyi klonladıktan sonra gerekli paketleri yüklemek için:


bash
Copy Code
dotnet restore
### 3. Veritabanı Ayarları

appsettings.json dosyasındaki veritabanı bağlantı ayarlarını kendi veritabanınıza göre güncelleyin:


json
Copy Code
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
### 4. Veritabanı Migrations ve Güncellemeleri

Veritabanı tablolarını oluşturmak için:


bash
Copy Code
dotnet ef migrations add InitialCreate
dotnet ef database update
### 5. Uygulamayı Çalıştırma

Projeyi çalıştırmak için:


bash
Copy Code
dotnet run
### 6. Admin Kullanıcıyla Giriş Yapma

Projeyi başlattıktan sonra varsayılan admin kullanıcıyı kullanarak giriş yapabilirsiniz:



Kullanıcı Adı: admin@admin.com

Şifre: Admin123!


Kullanım

Kitap Yönetimi

Kitap Ekleme

Yeni kitap eklemek için aşağıdaki URL'yi kullanın:



URL: https://localhost:5001/Book/Create


Kitap Güncelleme

Kitap güncellemek için ID'sini kullanarak aşağıdaki URL'yi kullanın:



URL: https://localhost:5001/Book/Update/{id}


Kitap Silme

Kitap silmek için ID'sini kullanarak aşağıdaki URL'yi kullanın:



URL: https://localhost:5001/Book/Delete/{id}


Kitap Görüntüleme

Tüm kitapları listelemek için aşağıdaki URL'yi kullanın:



URL: https://localhost:5001/Book


Kullanıcı Yönetimi (Sadece Admin)

Kullanıcıları yönetmek için aşağıdaki URL'yi kullanın:



URL: https://localhost:5001/User


Rol Yönetimi (Sadece Admin)

Rolleri yönetmek için aşağıdaki URL'yi kullanın:



URL: https://localhost:5001/Role


Projede Kullanılan Teknolojiler


ASP.NET Core MVC

Entity Framework Core

ASP.NET Core Identity

SQL Server
