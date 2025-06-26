namespace KnowCloud.Utility
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            // Manejo de excepciones y seguridad
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Autenticación y autorización en el orden correcto
            app.UseAuthentication();
            app.UseAuthorization();
            // Manejo de errores HTTP
            app.UseStatusCodePagesWithRedirects("/Account/Denied");
                        // Configuración de rutas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}"
            );
            // Carga de archivos estáticos si existen
            app.MapStaticAssets();
        }
    }
}
