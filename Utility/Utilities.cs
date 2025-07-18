﻿namespace KnowCloud.Utility
{
    public class Utilities
    {
        public static string AuthAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string CouponAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public const string TokenCookie = "JWTToken";
        public const string RoleAdmin = "ADMINISTRADOR";
        public const string RoleCustomer = "CUSTOMER";
        public enum ApiType {
            GET,
            POST,
            PUT,
            DELETE
        }
        /// <summary>
        /// Esta  propiedad nos sirve para poder recibir dos tipos de datos 1.-Json y 2.-Un archivo en binario
        /// </summary>
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
        public const string Status_Pending = "Pendiente";
        public const string Status_Approved = "Aprovado";
        public const string Status_ReadyForPickup = "Listo para Recoger";
        public const string Status_Completed = "Completo";
        public const string Status_Refunded = "Regresado";
        public const string Status_Cancelled = "Cancelado";
    }
}
