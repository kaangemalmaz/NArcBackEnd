namespace NArcBackEnd.Core.CrossCuttingConcerns.Caching
{
    // cache - 1
    public interface ICacheManager
    {
        T Get<T> (string key); // object türünde keye göre cache almak
        object Get (string key); // keye göre cache almak
        void Add(string key, object data, int duration); //duration cachede bekleyeceği süre
        bool IsAdd(string key); // ekli kayıt varsa yeniden cache oluşturmamak için
        void Remove(string key); // o keydeki tüm cacheleri silmek
        void RemoveByPattern(string pattern); // özellikli patterne göre cache temizleme
    }
}
