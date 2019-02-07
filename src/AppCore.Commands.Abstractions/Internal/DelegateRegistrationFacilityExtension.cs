﻿// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using AppCore.DependencyInjection.Builder;
using AppCore.DependencyInjection.Facilities;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    internal class DelegateRegistrationFacilityExtension : FacilityExtension<ICommandsFacility>
    {
        private readonly Type _contractType;
        private readonly Action<IRegistrationBuilder, ICommandsFacility> _action;

        public DelegateRegistrationFacilityExtension(Type contractType, Action<IRegistrationBuilder, ICommandsFacility> action)
        {
            _contractType = contractType;
            _action = action;
        }

        protected override void RegisterComponents(IComponentRegistry registry, ICommandsFacility facility)
        {
            _action(registry.Register(_contractType), facility);
        }
    }
}