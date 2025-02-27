using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Parser.Files;

namespace AssetRipper.Core.Classes.Shader.Parameters
{
	public sealed class ConstantBuffer : IAssetReadable
	{
		/// <summary>
		/// 2017.3 and greater
		/// </summary>
		public static bool HasStructParams(UnityVersion version) => version.IsGreaterEqual(2017, 3);

		/// <summary>
		/// If on 2021, 2021.1.4 and greater. Otherwise, 2020.3.2 and greater.
		/// Not present in 2021.1.0 - 2021.1.3
		/// </summary>
		public static bool HasIsPartialCB(UnityVersion version) => version.Major == 2021 ? version.IsGreaterEqual(2021, 1, 4) : version.IsGreaterEqual(2020, 3, 2);

		public ConstantBuffer() { }

		public ConstantBuffer(string name, MatrixParameter[] matrices, VectorParameter[] vectors, StructParameter[] structs, int usedSize)
		{
			Name = name;
			NameIndex = -1;
			MatrixParams = matrices;
			VectorParams = vectors;
			StructParams = structs;
			Size = usedSize;
			IsPartialCB = false;
		}

		public void Read(AssetReader reader)
		{
			NameIndex = reader.ReadInt32();
			MatrixParams = reader.ReadAssetArray<MatrixParameter>();
			VectorParams = reader.ReadAssetArray<VectorParameter>();
			if (HasStructParams(reader.Version))
			{
				StructParams = reader.ReadAssetArray<StructParameter>();
			}
			Size = reader.ReadInt32();
			if (HasIsPartialCB(reader.Version))
			{
				IsPartialCB = reader.ReadBoolean();
				reader.AlignStream();
			}
		}

		public string Name { get; set; }
		public int NameIndex { get; set; }
		public MatrixParameter[] MatrixParams { get; set; }
		public VectorParameter[] VectorParams { get; set; }
		public StructParameter[] StructParams { get; set; }
		public int Size { get; set; }
		public bool IsPartialCB { get; set; }
	}
}
