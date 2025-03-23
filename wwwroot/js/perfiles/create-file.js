$(document).ready(function () {
    $("#btnGuardar").click(function (e) {
        e.preventDefault(); // Evita la recarga de la página

        var fileInput = $("#txtFile")[0];
        var files = fileInput.files;

        if (files.length === 0) {
            alert("Por favor, selecciona una imagen antes de subir.");
            return;
        }

        // Filtrar solo imágenes y permitir solo un archivo
        var file = Array.from(files).find(f => f.type.startsWith("image/"));
        if (!file) {
            alert("Solo se permiten archivos de imagen.");
            return;
        }

        var formData = new FormData();
        formData.append("file", file);

        // ID dinámico (puedes cambiarlo según tus necesidades)
        var userId = 123; // Reemplazar con el ID real
        /// ID dinámico obtenido desde un atributo del botón
        ///var userId = $("#btnGuardar").data("user-id");
        $.ajax({
            url: `/api/archivos/${userId}`,
            type: "Post",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                alert("Archivo subido correctamente.");
            },
            error: function (xhr, status, error) {
                alert("Error al subir el archivo: " + xhr.responseText);
            }
        });
    });
});
