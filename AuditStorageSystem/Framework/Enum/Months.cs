using AuditStorageSystem.Framework.Attributes;

namespace AuditStorageSystem.Framework.Enum
{
    public enum Months
    {
        [StringMapping("January")]
        Jan = 1,
        [StringMapping("February")]
        Feb,
        [StringMapping("March")]
        Mar,
        [StringMapping("April")]
        Apr,
        [StringMapping("May")]
        May,
        [StringMapping("June")]
        June,
        [StringMapping("July")]
        July,
        [StringMapping("August")]
        Aug,
        [StringMapping("September")]
        Sept,
        [StringMapping("October")]
        Oct,
        [StringMapping("November")]
        Nov,
        [StringMapping("December")]
        Dec,
    }
}