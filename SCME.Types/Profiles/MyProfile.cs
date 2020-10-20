﻿using PropertyChanged;
using SCME.Types.BaseTestParams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SCME.Types.Annotations;
using System.Xml.Serialization;

namespace SCME.Types.Profiles
{
    [DataContract(Namespace = "http://proton-electrotex.com/SCME")]
    [AddINotifyPropertyChangedInterface]
    public class ProfileDeepData
    {
        [XmlIgnore]
        public ObservableCollection<BaseTestParametersAndNormatives> _testParametersAndNormatives { get; set; } = new ObservableCollection<BaseTestParametersAndNormatives>();

        [DataMember]
        public ObservableCollection<BaseTestParametersAndNormatives> TestParametersAndNormatives
        {
            get
            {
                return _testParametersAndNormatives;
            }
            set
            {
                _testParametersAndNormatives = value;
                if(_testParametersAndNormatives == null)
                    return;
                foreach (var i in _testParametersAndNormatives)
                    i.DutPackageType = _dutPackageType;
            }
        }

        #region Comutation
        [DataMember]
        public Commutation.ModuleCommutationType CommutationType { get; set; } = Commutation.ModuleCommutationType.Direct;

        //[DataMember]
        //public DutPackageType DutPackageType { get; set; }

        [XmlIgnore]
        private DutPackageType _dutPackageType;

        [DataMember]
        public DutPackageType DutPackageType
        {
            get
            {
                if ((int)_dutPackageType == 0)
                    _dutPackageType = DutPackageType.A1;
                return _dutPackageType;
            }
            set
            {
                _dutPackageType = value;
                if (TestParametersAndNormatives == null)
                    return;
                foreach (var i in TestParametersAndNormatives)
                    i.DutPackageType = value;
            }
        }

        #endregion
        #region Clamping
        [DataMember]
        public Clamping.ClampingForce ClampingForce { get; set; }
        [DataMember]
        public float ParameterClamp { get; set; } = 5;
        [DataMember]
        public bool IsHeightMeasureEnabled { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Temperature { get; set; }
        #endregion
    
    }

    [DataContract(Namespace = "http://proton-electrotex.com/SCME")]
    [AddINotifyPropertyChangedInterface]
    public class MyProfile
    {
        public MyProfile(int id, string name, Guid key, int version, DateTime timestamp)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Key = key;
            Version = version;
            Timestamp = timestamp;
            Id = id;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid Key { get; set; }

        [DataMember]
        public int Version { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public ObservableCollection<MyProfile> Children { get; set; } = new ObservableCollection<MyProfile>();

        [DataMember]
        public ProfileDeepData DeepData { get; set; } = new ProfileDeepData();

        public bool IsTop { get; set; }
        public MyProfile Parent { get; set; }

        public MyProfile GenerateNextVersion(ProfileDeepData profileDeepData, string newName)
        {
            var newProfile = new MyProfile(Id, newName, Guid.NewGuid(), Version+ 1, DateTime.Now)
            {
                DeepData = profileDeepData,
                IsTop = true,
            };
            
            return newProfile;
        }

        public Profile ToProfile() => new Profile()
        {
            Id = Id,
            Name = Name,
            Key = Key,
            Version = Version,
            Timestamp = Timestamp,
            
            IsHeightMeasureEnabled = DeepData.IsHeightMeasureEnabled,
            ParametersClamp = DeepData.ParameterClamp,
            Height = DeepData.Height,
            Temperature = DeepData.Temperature,
            ParametersComm = DeepData.CommutationType,
            DutPackageType = DeepData.DutPackageType,
            
            TestParametersAndNormatives = DeepData.TestParametersAndNormatives
        };

        public MyProfile(Profile profile) : this(profile.Id, profile.Name, profile.Key, profile.Version, profile.Timestamp)
        {        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is MyProfile profile))
                return false;
            return Id == profile.Id;
        }

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => Id;

        public class ProfileByKeyEqualityComparer : IEqualityComparer<MyProfile>
        {
            public bool Equals(MyProfile p1, MyProfile p2)
            {
                if (p2 == null && p1 == null)
                    return false;
                if (p1 == null || p2 == null)
                    return false;
                return p1.Key == p2.Key;
            }

            public int GetHashCode(MyProfile p)
            {
                return p.Key.GetHashCode();
            }
        }
        
        public class ProfileByVersionTimeEqualityComparer : IEqualityComparer<MyProfile>
        {
            public bool Equals(MyProfile p1, MyProfile p2)
            {
                if (p2 == null && p1 == null)
                    return false;
                if (p1 == null || p2 == null)
                    return false;
                return p1.Name == p2.Name && p1.Version == p2.Version && p1.Timestamp == p2.Timestamp;
            }

            public int GetHashCode(MyProfile p)
            {
                return p.Key.GetHashCode();
            }
        }
    }
}
