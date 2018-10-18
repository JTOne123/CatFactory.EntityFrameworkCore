﻿using System.Collections.Generic;
using System.Linq;
using CatFactory.Mapping;
using CatFactory.OOP;

namespace CatFactory.EntityFrameworkCore
{
    public static class ConstraintExtensions
    {
        public static PropertyDefinition GetParentNavigationProperty(this ForeignKey foreignKey, ITable table, EntityFrameworkCoreProject project)
        {
            var propertyType = string.Join(".", (new string[] { project.Name, project.Namespaces.EntityLayer, project.Database.HasDefaultSchema(table) ? string.Empty : table.Schema, table.GetEntityName() }).Where(item => !string.IsNullOrEmpty(item)));

            var selection = project.GetSelection(table);

            return new PropertyDefinition(propertyType, string.Format("{0}Fk", table.GetEntityName()))
            {
                IsVirtual = selection.Settings.DeclareNavigationPropertiesAsVirtual,
                Attributes = selection.Settings.UseDataAnnotations ? new List<MetadataAttribute> { new MetadataAttribute("ForeignKey", string.Format("\"{0}\"", string.Join(",", foreignKey.Key))) } : null
            };
        }
    }
}
