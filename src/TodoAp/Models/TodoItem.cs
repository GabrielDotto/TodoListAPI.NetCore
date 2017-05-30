using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    [Table("Tarefa")]
    public class TodoItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }  
        public DateTime? DataConclusao { get; set; }
    }
}