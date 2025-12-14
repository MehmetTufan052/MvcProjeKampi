using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IIstatistikService
    {
        int ToplamKategoriSayisi();
        int YazilimBaslikSayisi();
        int AHarfiYazarSayisi();
        string EnFazlaBaslikKategori();
        int KategoriDurumFarki();
    }
}
