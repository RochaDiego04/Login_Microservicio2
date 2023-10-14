using Ejemplo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Ejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeticionesController : ControllerBase
    {

        static List<Producto> productos = new List<Producto>()
            {
            new Producto() { id = 1, nombre = "Laptop", cantidad = 2, precio = 1000 },
            new Producto() { id = 2, nombre = "Mouse", cantidad = 4, precio = 20 },
            new Producto() { id = 3, nombre = "Teclado",cantidad = 1, precio = 30 }
            };

        [HttpGet("productos")]
        public IActionResult GetProductos([FromQuery] int page)
        {
            MiRespuesta respuesta = new MiRespuesta();
            respuesta.Ok = true;

            respuesta.Datos = productos;

            Console.WriteLine(productos);

            return Ok(respuesta);
        }

        [HttpPost("PeticionProductoPost")]
        public IActionResult PruebaPost([FromBody] Producto producto)
        {
            MiRespuesta respuesta = new MiRespuesta();
            if (producto == null)
            {
                return BadRequest();
            }
            respuesta.Ok = true;
            respuesta.Datos = producto;
            productos.Add(producto);
            Console.WriteLine(productos);
            return Ok(respuesta);
        }


        [HttpPost("PeticionPost")]
        public IActionResult PruebaPost([FromBody]Login usuario)
        {
            MiRespuesta respuesta = new MiRespuesta();
            if (usuario == null)
            {
                return BadRequest();
            }

            if (usuario.Usuario=="admin" && usuario.Contrasenia == "123")
            {
                respuesta.Ok = true;
                usuario.Token = "aaaa";
                respuesta.Datos = usuario;
                return Ok(respuesta);
            }

            respuesta.Ok = false;
            respuesta.Mensaje = "Error: usuario no  valido!!";
            return Ok(respuesta);
        }

        [HttpPut("PeticionPut")]
        public IActionResult PruebaPut([FromBody] Login usuario)
        {
            MiRespuesta respuesta = new MiRespuesta();
            if (usuario == null)
            {
                return BadRequest();
            }

            if (usuario.Usuario == "admin" && usuario.Contrasenia == "123")
            {
                respuesta.Ok = true;
                usuario.Contrasenia = "456";
                respuesta.Datos = usuario;
                
                return Ok(respuesta);
            }

            respuesta.Ok = false;
            respuesta.Mensaje = "Error: No se puede actualizar la contraseña!!";
            return Ok(respuesta);
        }

        [HttpDelete("PeticionDelete")]
        public IActionResult PeticionDelete(int idProducto)
        {
            MiRespuesta respuesta = new MiRespuesta();
            // Buscar el producto por su id
            Console.WriteLine(productos);
            var producto = productos.Find(p => p.id == idProducto);
            // Si no se encuentra, retornar NotFound
            if (producto == null)
            {
                return NotFound();
            }
            // Eliminar el producto de la lista
            productos.Remove(producto);
            // Retornar Ok con la respuesta
            respuesta.Ok = true;
            respuesta.Datos = producto;
            return Ok(respuesta);
        }

        [HttpPut("PeticionProductoPut")]
        public IActionResult PruebaPut([FromBody] Producto producto)
        {
            MiRespuesta respuesta = new MiRespuesta();
            if (producto == null)
            {
                return BadRequest();
            }

            // Buscar el producto por su id
            var productoExistente = productos.Find(p => p.id == producto.id);

            if (productoExistente == null)
            {
                return NotFound();
            }

            // Actualizar el producto existente con los datos del producto recibido
            productoExistente.nombre = producto.nombre;
            productoExistente.cantidad = producto.cantidad;
            productoExistente.precio = producto.precio;

            respuesta.Ok = true;
            respuesta.Datos = productoExistente;

            return Ok(respuesta);
        }

    }
}
