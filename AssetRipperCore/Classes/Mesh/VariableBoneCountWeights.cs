﻿using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;
using AssetRipper.Core.YAML.Extensions;
using System;
using System.Linq;

namespace AssetRipper.Core.Classes.Mesh
{
	public sealed class VariableBoneCountWeights : IVariableBoneCountWeights
	{
		public VariableBoneCountWeights Convert(IExportContainer container)
		{
			VariableBoneCountWeights instance = new VariableBoneCountWeights();
			instance.Data = Data.ToArray();
			return instance;
		}

		public void Read(AssetReader reader)
		{
			Data = reader.ReadUInt32Array();
		}

		public void Write(AssetWriter writer)
		{
			Data.Write(writer);
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(DataName, Data.ExportYAML(true));
			return node;
		}

		public uint[] Data { get; set; } = Array.Empty<uint>();

		public const string DataName = "m_Data";
	}
}
