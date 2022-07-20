namespace Curso.ComercioElectronico.WebApi.Controllers
{
    public class JwtConfiguration
    {

        /// <summary>
        /// Clave para firmar los tokens 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Identifica el proveedor de identidad que emitió el JWT.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Identifica la audiencia o receptores para lo que el JWT fue emitido.
        /// Cada servicio que recibe un JWT para su validación tiene que controlar la audiencia a la que el JWT está destinado.
        /// </summary>
        public string Audience { get; set; }

        public TimeSpan Expires { get; set; } = TimeSpan.FromMinutes(10);

    }
}
