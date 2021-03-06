﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SakuraFrpService.WebSocketShim {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SakuraFrpService.WebSocketShim.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 The requested security protocol is not supported. 的本地化字符串。
        /// </summary>
        internal static string net_securityprotocolnotsupported {
            get {
                return ResourceManager.GetString("net_securityprotocolnotsupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 This operation is not supported for a relative URI. 的本地化字符串。
        /// </summary>
        internal static string net_uri_NotAbsolute {
            get {
                return ResourceManager.GetString("net_uri_NotAbsolute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The WebSocket client request requested &apos;{0}&apos; protocol(s), but server is only accepting &apos;{1}&apos; protocol(s). 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_AcceptUnsupportedProtocol {
            get {
                return ResourceManager.GetString("net_WebSockets_AcceptUnsupportedProtocol", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 There is already one outstanding &apos;{0}&apos; call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time. 的本地化字符串。
        /// </summary>
        internal static string net_Websockets_AlreadyOneOutstandingOperation {
            get {
                return ResourceManager.GetString("net_Websockets_AlreadyOneOutstandingOperation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The WebSocket has already been started. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_AlreadyStarted {
            get {
                return ResourceManager.GetString("net_WebSockets_AlreadyStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The message type &apos;{0}&apos; is not allowed for the &apos;{1}&apos; operation. Valid message types are: &apos;{2}, {3}&apos;. To close the WebSocket, use the &apos;{4}&apos; operation instead.  的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_Argument_InvalidMessageType {
            get {
                return ResourceManager.GetString("net_WebSockets_Argument_InvalidMessageType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The argument must be a value greater than {0}. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_ArgumentOutOfRange_TooSmall {
            get {
                return ResourceManager.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 An internal WebSocket error occurred. Please see the innerException, if present, for more details.  的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_Generic {
            get {
                return ResourceManager.GetString("net_WebSockets_Generic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The WebSocket protocol &apos;{0}&apos; is invalid because it contains the invalid character &apos;{1}&apos;. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidCharInProtocolString {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidCharInProtocolString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The close status code &apos;{0}&apos; is reserved for system use only and cannot be specified when calling this method. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidCloseStatusCode {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidCloseStatusCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The close status description &apos;{0}&apos; is too long. The UTF8-representation of the status description must not be longer than {1} bytes. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidCloseStatusDescription {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidCloseStatusDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Empty string is not a valid subprotocol value. Please use \&quot;null\&quot; to specify no value. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidEmptySubProtocol {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidEmptySubProtocol", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The &apos;{0}&apos; header value &apos;{1}&apos; is invalid. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidResponseHeader {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidResponseHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The WebSocket is in an invalid state (&apos;{0}&apos;) for this operation. Valid states are: &apos;{1}&apos; 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidState {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The &apos;{0}&apos; instance cannot be used for communication because it has been transitioned into the &apos;{1}&apos; state. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_InvalidState_ClosedOrAborted {
            get {
                return ResourceManager.GetString("net_WebSockets_InvalidState_ClosedOrAborted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Duplicate protocols are not allowed: &apos;{0}&apos;. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_NoDuplicateProtocol {
            get {
                return ResourceManager.GetString("net_WebSockets_NoDuplicateProtocol", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The WebSocket is not connected. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_NotConnected {
            get {
                return ResourceManager.GetString("net_WebSockets_NotConnected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The close status description &apos;{0}&apos; is invalid. When using close status code &apos;{1}&apos; the description must be null. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_ReasonNotNull {
            get {
                return ResourceManager.GetString("net_WebSockets_ReasonNotNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Only Uris starting with &apos;ws://&apos; or &apos;wss://&apos; are supported. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_Scheme {
            get {
                return ResourceManager.GetString("net_WebSockets_Scheme", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The WebSocket protocol is not supported on this platform. 的本地化字符串。
        /// </summary>
        internal static string net_WebSockets_UnsupportedPlatform {
            get {
                return ResourceManager.GetString("net_WebSockets_UnsupportedPlatform", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Unable to connect to the remote server 的本地化字符串。
        /// </summary>
        internal static string net_webstatus_ConnectFailure {
            get {
                return ResourceManager.GetString("net_webstatus_ConnectFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The base stream is not readable. 的本地化字符串。
        /// </summary>
        internal static string NotReadableStream {
            get {
                return ResourceManager.GetString("NotReadableStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The base stream is not writeable. 的本地化字符串。
        /// </summary>
        internal static string NotWriteableStream {
            get {
                return ResourceManager.GetString("NotWriteableStream", resourceCulture);
            }
        }
    }
}
