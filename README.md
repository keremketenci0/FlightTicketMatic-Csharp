# FlightTicketMatic-Csharp

# Uçuş Biletmatik Uygulaması

Bu, C# ve Visual Studio kullanılarak geliştirilmiş bir konsol uygulamasıdır. Bu uygulama, kullanıcının bir uçuş için bilet satın almasını sağlar.

## Kullanım

Uygulamayı çalıştırmak için aşağıdaki adımları takip edin:

1. Projeyi Visual Studio'da açın.
2. Program.cs dosyasını bulun ve çalıştırın.
3. Konsol ekranında uygulamanın başlamasını bekleyin.

Uygulama başladığında, aşağıdaki adımları takip ederek bilet satın alabilirsiniz:

1. Eylem girin. Konsolda size bir mesaj gösterilecektir.
2. Bilet almak istediğiniz ülkeyi girin. Konsolda mevcut ülkeler listelenecektir ve sizden bir seçim yapmanız istenecektir.
3. Bilet almak istediğiniz şehri girin. Konsolda mevcut şehirler listelenecektir ve sizden bir seçim yapmanız istenecektir.
4. Bilet almak istediğiniz havalimanını girin. Konsolda mevcut havalimanları listelenecektir ve sizden bir seçim yapmanız istenecektir.
5. Bilet almak istediğiniz havayolunu girin. Konsolda mevcut havayolları listelenecektir ve sizden bir seçim yapmanız istenecektir.
6. Bilet almak istediğiniz uçuşu girin. Konsolda mevcut uçuşlar listelenecektir ve sizden bir seçim yapmanız istenecektir.
7. Adınızı girin. Konsolda size bir mesaj gösterilecektir.
8. Soyadınızı girin. Konsolda size bir mesaj gösterilecektir.
9. Koltuk seçim ekranı görüntülenecektir. Boş olan koltuk sayısı listelenecektir ve sizden bir seçim yapmanız istenecektir.
10. Bütün adımları tamamladıktan sonra, biletlerinizin detayları ekranda görüntülenecektir.

## Sınıflar

Bu proje aşağıdaki sınıflardan oluşmaktadır:

- Program.cs: Ana uygulama dosyasıdır ve kullanıcıyla etkileşim sağlar.
- Country.cs: Ülkeleri temsil eden sınıfı içerir.
- City.cs: Şehirleri temsil eden sınıfı içerir.
- Airport.cs: Havalimanlarını temsil eden sınıfı içerir.
- Airline.cs: Havayollarını temsil eden sınıfı içerir.
- Flight.cs: Uçuşları temsil eden sınıfı içerir.
- Reservation.cs: Bilet rezervasyonlarını temsil eden sınıfı içerir.

## Önemli Notlar

- Bu uygulama sadece temsili bir amaç taşımaktadır ve gerçek bir bilet rezervasyon sistemi olarak kullanılamaz.
- Uygulamanın veri tabanı üzerinde veri depolama işlemi gerçekleştirmez. Tüm veriler "Data/Flights.json" dosyasından çekilir ve "Data/Reservations.json" dosyasına yazılır.
- Bu uygulama, kullanıcıların doğru girişler yapmasını sağlamak için try-catch yöntemini kullanır. Hatalı girişlerde uygulama çökmez, bunun yerine hata mesajı gösterir ve kullanıcıyı tekrar giriş yapmaya yönlendirir.
- Uygulama, kullanıcının eski seçimlere geri dönebilmesi için while döngüsü kullanır. Kullanıcı istediği zaman geri dönüp farklı bir seçim yapabilir ve bilet işlemlerini yeniden başlatabilir.
- Bu değişiklikler, kullanıcı deneyimini iyileştirmek ve hata yönetimini güçlendirmek için yapılmıştır.

## Modelleme
// Draw.io


// Paint



