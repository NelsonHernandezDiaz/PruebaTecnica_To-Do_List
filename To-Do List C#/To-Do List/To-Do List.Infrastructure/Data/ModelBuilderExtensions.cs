using Microsoft.EntityFrameworkCore;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Infrastructure.Data
{
	/// <summary>
	/// Base de datos semilla para tener con que empezar a realizar pruebas.
	/// </summary>
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tarea>()
				.HasData(
					new Tarea
					{
						Id = 1,
						Nombre = "Aprender C#",
						Estado = false
					},
					new Tarea
					{
						Id = 2,
						Nombre = "Aprender HTML"
					},
					new Tarea
					{
						Id = 3,
						Nombre = "Aprender CSS"
					},
					new Tarea
					{
						Id = 4,
						Nombre = "Aprender JS"
					},
					new Tarea
					{
						Id = 5,
						Nombre = "Aprender React"
					});
		}
	}
}
