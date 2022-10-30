using System.ComponentModel.DataAnnotations;

namespace TMinggu2.Models
{
    public class DPribadi
    {
        public int Id { get; set; }

        [Required]
        public int NIK { get; set; }
        [Required]
        public String NamaLengkap { get; set; }
        public String JenisKelamin { get; set; }
        public DateTime TanggalLahir { get; set; }
        public String Alamat { get; set; }
        public String Negara { get; set; }
    }
}
