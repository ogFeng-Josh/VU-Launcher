﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VULauncher.Models.PresetProviders;
using VULauncher.ViewModels.Items;

namespace VULauncher.Models.Entities.Extensions
{
    public static class ClientPresetExtensions
    {
        public static ClientPresetItem ToItem(this ClientPreset entity)
        {
            return new ClientPresetItem()
            {
                Id = entity.Id,
                Name = entity.Name,
                ClientParamsPreset = ClientParamsPresetsProvider.Instance.FindPresetItemById(entity.Id),
                FrequencyType = entity.FrequencyType,
                OpenConsole = entity.OpenConsole,
                SendRuntimeErrorDumps = entity.SendRuntimeErrorDumps,
                IsDirty = false,
            };
        }

        public static ClientPreset ToEntity(this ClientPresetItem item)
        {
            return new ClientPreset()
            {
                Id = item.Id,
                Name = item.Name,
                ClientParamsPresetId = item.ClientParamsPreset.Id,
                FrequencyType = item.FrequencyType,
                OpenConsole = item.OpenConsole,
                SendRuntimeErrorDumps = item.SendRuntimeErrorDumps,
            };
        }

        public static List<ClientPresetItem> ToItemList(this IEnumerable<ClientPreset> entities)
        {
            return entities.Select(e => e.ToItem()).ToList();
        }

        public static List<ClientPreset> ToEntityList(this IEnumerable<ClientPresetItem> items)
        {
            return items.Select(e => e.ToEntity()).ToList();
        }
    }
}