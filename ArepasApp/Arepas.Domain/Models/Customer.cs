﻿using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Models;

public class Customer: EntityBase
{
    
    [EmailAddress(ErrorMessage ="El Campo UserEmail Debe Ser un Email Valido")]
    [StringLength(250, ErrorMessage ="La Longitud Maxima para el Campo UserEmail es de 250 Caracteres")]
    public string UserEmail { get; set; } = null!;

    
    [StringLength(100, ErrorMessage = "La Longitud Maxima para Nombre Completo del Cliente es de 100 Caracteres")]
    public string FullName { get; set; } = null!;

    
    [StringLength(250, ErrorMessage = "La Longitud Maxima para la Direccion del Cliente es de 250 Caracteres")]
    public string Address { get; set; } = null!;

    
    [StringLength(50, ErrorMessage = "La Longitud Maxima para el Telefono del cliente es de 50 Caracteres")]
    public string PhoneNumber { get; set; } = null!;

    
    [StringLength(50, ErrorMessage = "La Longitud Maxima para La Contraseña es de 50 Caracteres")]
    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}