using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using LinXi_Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LinXi_Common
{
    /// <summary>
    ///头部(header), 荷载(Payload), 和签名(Signature)
    ///签名信息由三部分组成：header(base64后的)、payload(base64后的)、secret
    /// </summary>
    public class JWTTool
    {
        private static IConfiguration Configuration { get; set; }
        private static ILogger Logger { get; set; }

        /// <summary>
        /// 注入配置
        /// </summary>
        /// <param name="configuration"></param>
        public JWTTool(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        /// <summary>
        /// 密钥
        /// </summary>
        private static string secretkey { get { return Configuration.GetSection("secretkey").Value; } }

        /// <summary>
        /// JWT加密
        /// </summary>
        /// <param name="lxUsers"></param>
        /// <returns></returns>
        public string EncodingToken(LxUsers lxUsers)
        {
            var payload = new Dictionary<string, dynamic>
            {
                {"UserID", lxUsers.LxUsersId},//用于存放当前登录人账号ID
                {"UserName", lxUsers.LxUsersLoginName}//用于存放当前登录人账号名
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secretkey);
        }

        //public string DecodingToken(string token)
        //{
        //    try
        //    {
        //        IJsonSerializer serializer = new JsonNetSerializer();
        //        IDateTimeProvider provider = new UtcDateTimeProvider();
        //        IJwtValidator validator = new JwtValidator(serializer, provider);
        //        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
        //        //加密串，密钥，是否完整校验
        //        var json = decoder.Decode(token, secretkey, verify: true);
        //        return json;
        //    }
        //    catch (TokenExpiredException)
        //    {
        //        Logger.LogError("Token过期！");
        //        return default;
        //    }
        //    catch (SignatureVerificationException)
        //    {
        //        Logger.LogError("签名验证失败，数据可能被篡改");
        //        return default;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex.Message);
        //        return default;
        //    }
        //}

        /// <summary>
        /// JWT-RS256 解密
        /// </summary>
        /// <param name="token">要解密的token</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static Dictionary<string, object> DecodingToken(string token, string publicKey = "D97A68610A99865774B4943D4D95E060")
        {
            try
            {
                using (var rsa = GetRSACryptoServiceProvider(publicKey, false)) //读取公钥
                {
                    var payload = Jose.JWT.Decode<Dictionary<string, object>>(token, rsa);
                    return payload;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 私钥 or 公钥 返回参数
        /// </summary>
        /// <param name="key">私钥 or 公钥</param>
        /// <returns></returns>
        private static RSACryptoServiceProvider GetRSACryptoServiceProvider(string key, bool isprivate)
        {
            try
            {
                RSAParameters rsaParams;
                if (isprivate)
                {
                    using (var sr = new StringReader(key))
                    {
                        // use BouncyCastle to convert the key to RSA parameters
                        //需要引用包 BouncyCastle
                        var pemReader = new PemReader(sr);
                        var keyPair = pemReader.ReadObject() as RsaPrivateCrtKeyParameters;
                        if (keyPair == null)
                        {
                            //没有读到Privatekey
                        }
                        rsaParams = DotNetUtilities.ToRSAParameters(keyPair);
                    }
                }
                else
                {
                    using (var sr = new StringReader(key))
                    {
                        var pemReader = new PemReader(sr);
                        var keyPair = pemReader.ReadObject() as RsaKeyParameters;
                        if (keyPair == null)
                        {
                            //没有读到Publickey
                        }
                        rsaParams = DotNetUtilities.ToRSAParameters(keyPair);
                    }
                }
                var rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(rsaParams);
                return rsa;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}