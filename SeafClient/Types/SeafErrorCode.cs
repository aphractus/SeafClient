﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SeafClient.Types
{
    /// <summary>
    /// Possible status codes returned by the Seafile API
    /// These are in fact HTTP status codes
    /// </summary>
    public enum SeafErrorCode
    {
        NoDetails = -1,
        InvalidCredentials = 1,
        PathDoesNotExist = 2,     
        FileNotFound = 3,
        EncryptedRepo_PasswordNotProvided = 4,
        InvalidToken = 5,
        OutOfQuota = 6,
        NotEnoughPermissions = 7        
    }

    /// <summary>
    /// Represents a seafile error which consists
    /// of a HTTP status code and a context-sensitive seafile error code if available
    /// </summary>
    public class SeafError
    {
        /// <summary>
        /// The HTTP status code received from the seafile server
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; private set; }

        private String httpStatusDescription = "";

        /// <summary>
        /// A more detailed error message in the seafile context if available
        /// </summary>
        public SeafErrorCode SeafErrorCode { get; private set; }

        public SeafError(HttpStatusCode httpStatusCode, SeafErrorCode seafErrorCode)
        {
            HttpStatusCode = httpStatusCode;
            HttpResponseMessage r = new HttpResponseMessage(HttpStatusCode);
            httpStatusDescription = r.ReasonPhrase;
            SeafErrorCode = seafErrorCode;
        }

        public string GetErrorMessage()
        {
            switch (SeafErrorCode)
            {
                case SeafErrorCode.InvalidCredentials:
                    return "The given login credentials are invalid.";
                case SeafErrorCode.PathDoesNotExist:
                    return "The path does not exist.";
                case SeafErrorCode.FileNotFound:
                    return "The file does not exist.";
                case SeafErrorCode.EncryptedRepo_PasswordNotProvided:
                    return "The repository is enrypted but password was not provided.";
                case SeafErrorCode.InvalidToken:
                    return "The token is invalid.";
                case SeafErrorCode.OutOfQuota:
                    return "The user ran out of quota.";
                case SeafErrorCode.NotEnoughPermissions:
                    return "Not enough permissions to execute this request.";                
                default:
                    // print the http status code
                    return httpStatusDescription;
            }
        }
    }
}
