﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Currency", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Currency : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExchangeResponse", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class ExchangeResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private bool FoundField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.ServiceReference.Exchange ExchangeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool Found {
            get {
                return this.FoundField;
            }
            set {
                if ((this.FoundField.Equals(value) != true)) {
                    this.FoundField = value;
                    this.RaisePropertyChanged("Found");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public Client.ServiceReference.Exchange Exchange {
            get {
                return this.ExchangeField;
            }
            set {
                if ((object.ReferenceEquals(this.ExchangeField, value) != true)) {
                    this.ExchangeField = value;
                    this.RaisePropertyChanged("Exchange");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Exchange", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Exchange : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private double CurrentToOtherField;
        
        private double OtherToCurrentField;
        
        private int PercentageField;
        
        private double CurrentToOtherOldField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TrendField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public double CurrentToOther {
            get {
                return this.CurrentToOtherField;
            }
            set {
                if ((this.CurrentToOtherField.Equals(value) != true)) {
                    this.CurrentToOtherField = value;
                    this.RaisePropertyChanged("CurrentToOther");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public double OtherToCurrent {
            get {
                return this.OtherToCurrentField;
            }
            set {
                if ((this.OtherToCurrentField.Equals(value) != true)) {
                    this.OtherToCurrentField = value;
                    this.RaisePropertyChanged("OtherToCurrent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Percentage {
            get {
                return this.PercentageField;
            }
            set {
                if ((this.PercentageField.Equals(value) != true)) {
                    this.PercentageField = value;
                    this.RaisePropertyChanged("Percentage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public double CurrentToOtherOld {
            get {
                return this.CurrentToOtherOldField;
            }
            set {
                if ((this.CurrentToOtherOldField.Equals(value) != true)) {
                    this.CurrentToOtherOldField = value;
                    this.RaisePropertyChanged("CurrentToOtherOld");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Trend {
            get {
                return this.TrendField;
            }
            set {
                if ((object.ReferenceEquals(this.TrendField, value) != true)) {
                    this.TrendField = value;
                    this.RaisePropertyChanged("Trend");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ServiceSoap")]
    public interface ServiceSoap {
        
        // CODEGEN: Контракт генерации сообщений с именем str из пространства имен http://tempuri.org/ не отмечен как обнуляемый
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCurrency", ReplyAction="*")]
        Client.ServiceReference.GetCurrencyResponse GetCurrency(Client.ServiceReference.GetCurrencyRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCurrencyRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCurrency", Namespace="http://tempuri.org/", Order=0)]
        public Client.ServiceReference.GetCurrencyRequestBody Body;
        
        public GetCurrencyRequest() {
        }
        
        public GetCurrencyRequest(Client.ServiceReference.GetCurrencyRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetCurrencyRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Client.ServiceReference.Currency str;
        
        public GetCurrencyRequestBody() {
        }
        
        public GetCurrencyRequestBody(Client.ServiceReference.Currency str) {
            this.str = str;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCurrencyResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCurrencyResponse", Namespace="http://tempuri.org/", Order=0)]
        public Client.ServiceReference.GetCurrencyResponseBody Body;
        
        public GetCurrencyResponse() {
        }
        
        public GetCurrencyResponse(Client.ServiceReference.GetCurrencyResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetCurrencyResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Client.ServiceReference.ExchangeResponse GetCurrencyResult;
        
        public GetCurrencyResponseBody() {
        }
        
        public GetCurrencyResponseBody(Client.ServiceReference.ExchangeResponse GetCurrencyResult) {
            this.GetCurrencyResult = GetCurrencyResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceSoapChannel : Client.ServiceReference.ServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<Client.ServiceReference.ServiceSoap>, Client.ServiceReference.ServiceSoap {
        
        public ServiceSoapClient() {
        }
        
        public ServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Client.ServiceReference.GetCurrencyResponse Client.ServiceReference.ServiceSoap.GetCurrency(Client.ServiceReference.GetCurrencyRequest request) {
            return base.Channel.GetCurrency(request);
        }
        
        public Client.ServiceReference.ExchangeResponse GetCurrency(Client.ServiceReference.Currency str) {
            Client.ServiceReference.GetCurrencyRequest inValue = new Client.ServiceReference.GetCurrencyRequest();
            inValue.Body = new Client.ServiceReference.GetCurrencyRequestBody();
            inValue.Body.str = str;
            Client.ServiceReference.GetCurrencyResponse retVal = ((Client.ServiceReference.ServiceSoap)(this)).GetCurrency(inValue);
            return retVal.Body.GetCurrencyResult;
        }
    }
}
