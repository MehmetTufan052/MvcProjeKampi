using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using System.Linq;

public class IstatistikManager : IIstatistikService
{
    private ICategoryDal _categoryDal;
    private IHeadingDal _headingDal;
    private IWriterDal _writerDal;

    // ✅ TEK constructor – DOLU
    public IstatistikManager()
    {
        _categoryDal = new EfCategoryDal();
        _headingDal = new EfHeadingDal();
        _writerDal = new EfWriterDal();
    }

    public int ToplamKategoriSayisi()
    {
        return _categoryDal.List().Count();
    }

    public int YazilimBaslikSayisi()
    {
        return _headingDal.List()
            .Count(x => x.Category.CategoryName == "Yazılım");
    }

    public int AHarfiYazarSayisi()
    {
        return _writerDal.List()
            .Count(x => x.WriterName.ToLower().Contains("a"));
    }

    public string EnFazlaBaslikKategori()
    {
        return _headingDal.List()
            .GroupBy(x => x.Category.CategoryName)
            .OrderByDescending(x => x.Count())
            .Select(x => x.Key)
            .FirstOrDefault();
    }

    public int KategoriDurumFarki()
    {
        int aktif = _categoryDal.List().Count(x => x.CategoryStatus);
        int pasif = _categoryDal.List().Count(x => !x.CategoryStatus);
        return aktif - pasif;
    }
}
