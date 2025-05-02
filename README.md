# Configuration Manager

İçerisinde dinamik olarak configuration parametreleri yönetecek bir kütüphane sunmaktadır.
Asp.NetCore mvc ile yazılmış bir configurasyonları listeleme/ekleme/güncelleme/silme işlemi yapılabilecek UI.
ConfigurationReader kütüphanesini test edip kullanmak için bir ConsoleApp bulunmaktadır.

ConsoleApp de bulunan connectionstring'i db dockerda olduğu için docker compose file içerisinde bulunan db servis adı ile değiştiriniz.

Ayrıca burada postgreSql üzerinden örneklendirme yapıldı fakat ConfigurationReader kütüphanesinin SqlServer, MongoDb, Redis desteğide bulunmakdatır.

Aşadağıdaki adımları takip ederek uygulamayı kurabilirsiniz.



# Configuration Management UI

Bu proje, uygulamalar için yapılandırma parametrelerini yönetmek amacıyla geliştirilen bir web arayüzüdür. Kullanıcılar, yapılandırma anahtarlarını, değerlerini, açıklamalarını ve durumlarını kolayca yönetebilirler.


## Başlangıç

Bu projeyi kendi bilgisayarınızda çalıştırabilmek için aşağıdaki adımları izleyebilirsiniz:

1. **Depoyu klonlayın:**
   ```bash
   git clone https://github.com/safaktan/ConfigurationManager.git
   ```

2. **Gerekli bağımlılıkları yükleyin (eğer gerekiyorsa):**
   - Docker ve Docker Compose'un yüklü olduğundan emin olun.

3. **Docker Compose ile başlatın:**
   Aşağıdaki komut ile projeyi başlatabilirsiniz:
   ```bash
   docker-compose up --build
   ```

4. **Uygulamaya erişim:**
   Web UI'ye `http://localhost:8080` adresinden erişebilirsiniz.


## Yapılandırma

Bu proje için gerekli olan bazı çevresel değişkenler:

- `ConnectionStrings__DefaultConnection`: PostgreSQL veritabanı bağlantı dizesi. Örnek:
  ```plaintext
  Host=postgres;Port=5432;Database=ConfigurationDb;Username=admin;Password=admin123
  ```
Bu yapılandırma, `docker-compose.yml` içinde belirtilmiştir.

## Kullanım
Web arayüzüne giriş yaptıktan sonra aşağıdaki adımları izleyebilirsiniz:

1. Yapılandırma parametresi ekleyin veya mevcut parametreyi güncelleyin.
2. Uygulama adı ve anahtar değerlerini düzenleyin.
3. Yapılandırma parametrelerinin durumunu `Aktif` veya `Pasif` olarak ayarlayın.

Uygulama kullanıcı dostu bir arayüz sunmaktadır, dolayısıyla veritabanındaki yapılandırmalar kolayca yönetilebilir.


