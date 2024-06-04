using System;
using System.Runtime.InteropServices;

namespace SDL2_gpuCS;

public unsafe static class SDL_Gpu
{
    private const string nativeLibName = "SDL_gpu";

    /* Enums */

    public enum PrimitiveType
    {
        PointList,
        LineList,
        LineStrip,
        TriangleList,
        TriangleStrip
    }

    public enum LoadOp
    {
        Load,
        Clear,
        DontCare
    }

    public enum StoreOp
    {
        Store,
        DontCare
    }

    public enum IndexElementSize
    {
        Sixteen,
        ThirtyTwo
    }

    public enum TextureFormat
    {
	/* Unsigned Normalized Float Color Formats */
	R8G8B8A8,
	B8G8R8A8,
	R5G6B5,
	A1R5G5B5,
	B4G4R4A4,
	A2R10G10B10,
    A2B10G10R10,
	R16G16,
	R16G16B16A16,
	R8,
	A8,
	/* Compressed Unsigned Normalized Float Color Formats */
	BC1,
	BC2,
	BC3,
	BC7,
	/* Signed Normalized Float Color Formats  */
	R8G8_SNORM,
	R8G8B8A8_SNORM,
	/* Signed Float Color Formats */
	R16_SFLOAT,
	R16G16_SFLOAT,
	R16G16B16A16_SFLOAT,
	R32_SFLOAT,
	R32G32_SFLOAT,
	R32G32B32A32_SFLOAT,
	/* Unsigned Integer Color Formats */
	R8_UINT,
	R8G8_UINT,
	R8G8B8A8_UINT,
	R16_UINT,
	R16G16_UINT,
	R16G16B16A16_UINT,
	/* SRGB Color Formats */
	R8G8B8A8_SRGB,
	B8G8R8A8_SRGB,
	/* Compressed SRGB Color Formats */
	BC3_SRGB,
	BC7_SRGB,
	/* Depth Formats */
	D16_UNORM,
	D24_UNORM,
	D32_SFLOAT,
	D24_UNORM_S8_UINT,
	D32_SFLOAT_S8_UINT
    }

    [Flags]
    public enum TextureUsageFlags
    {
        Sampler = 0x1,
        ColorTarget = 0x2,
        DepthStencil = 0x4,
        GraphicsStorage = 0x8,
        ComputeStorageRead = 0x20,
        ComputeStorageWrite = 0x40
    }

    public enum TextureType
    {
        TwoD,
        ThreeD,
        Cube
    }

    public enum SampleCount
    {
        One,
        Two,
        Four,
        Eight
    }

    public enum CubeMapFace
    {
        PositiveX,
        NegativeX,
        PositiveY,
        NegativeY,
        PositiveZ,
        NegativeZ
    }

    [Flags]
    public enum BufferUsageFlags
    {
        Vertex = 0x1,
        Index = 0x2,
        Indirect = 0x4,
        GraphicsStorage = 0x8,
        ComputeStorageRead = 0x20,
        ComputeStorageWrite = 0x40
    }

    [Flags]
    public enum TransferBufferMapFlags
    {
        Read = 0x1,
        Write = 0x2
    }

    public enum ShaderStage
    {
        Vertex,
        Fragment,
        Compute
    }

    public enum ShaderFormat
    {
        Invalid,
        SPIRV,
        DXBC,
        DXIL,
        MSL,
        METALLIB,
        SECRET
    }

    public enum VertexElementFormat
    {
        Uint,
        Float,
        Vector2,
        Vector3,
        Vector4,
        Color,
        Byte4,
        Short2,
        Short4,
        NormalizedShort2,
        NormalizedShort4,
        HalfVector2,
        HalfVector4
    }

    public enum VertexInputRate
    {
        Vertex,
        Instance
    }

    public enum FillMode
    {
        Fill,
        Line
    }

    public enum CullMode
    {
        None,
        Front,
        Back
    }

    public enum FrontFace
    {
        CounterClockwise,
        Clockwise
    }

    public enum CompareOp
    {
        Never,
        Less,
        Equal,
        LessOrEqual,
        Greater,
        NotEqual,
        GreaterOrEqual,
        Always
    }

    public enum StencilOp
    {
        Keep,
        Zero,
        Replace,
        IncrementAndClamp,
        DecrementAndClamp,
        Invert,
        IncrementAndWrap,
        DecrementAndWrap
    }

    public enum BlendOp
    {
        Add,
        Subtract,
        ReverseSubtract,
        Min,
        Max
    }

    public enum BlendFactor
    {
        Zero,
        One,
        SourceColor,
        OneMinusSourceColor,
        DestinationColor,
        OneMinusDestinationColor,
        SourceAlpha,
        OneMinusSourceAlpha,
        DestinationAlpha,
        OneMinusDestinationAlpha,
        ConstantColor,
        OneMinusConstantColor,
        SourceAlphaSaturate
    }

    [Flags]
    public enum ColorComponentFlags
    {
        R = 0x1,
        G = 0x2,
        B = 0x4,
        A = 0x8
    }

    public enum Filter
    {
        Nearest,
        Linear
    }

    public enum SamplerMipmapMode
    {
        Nearest,
        Linear
    }

    public enum SamplerAddressMode
    {
        Repeat,
        MirroredRepeat,
        ClampToEdge,
        ClampToBorder
    }

    public enum BorderColor
    {
        FloatTransparentBlack,
        IntTransparentBlack,
        FloatOpaqueBlack,
        IntOpaqueBlack,
        FloatOpaqueWhite,
        IntOpaqueWhite
    }

    public enum TransferUsage
    {
        Buffer,
        Texture
    }

    public enum PresentMode
    {
        VSync,
        Immediate,
        Mailbox
    }

    public enum SwapchainComposition
    {
        SDR,
        SDRLinear,
        HDRExtendedLinear,
        HDR10_ST2084
    }

    [Flags]
    public enum BackendFlags
    {
        Invalid = 0x0,
        Vulkan = 0x1,
        D3D11 = 0x2,
        Metal = 0x4,
        All = Vulkan | D3D11 | Metal
    }

    /* Structures */

    public struct DepthStencilValue
    {
        public float Depth;
        public uint Stencil;
    }

    public struct Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;
    }

    public struct Viewport
    {
        public float X;
        public float Y;
        public float W;
        public float H;
        public float MinDepth;
        public float MaxDepth;
    }

    public struct TextureSlice
    {
        public nint Texture;
        public uint MipLevel;
        public uint Layer;
    }

    public struct TextureRegion
    {
        public TextureSlice TextureSlice;
        public uint X;
        public uint Y;
        public uint Z;
        public uint W;
        public uint H;
        public uint D;
    }

    public struct BufferImageCopy
    {
        public uint BufferOffset;
        public uint BufferStride;
        public uint BufferImageHeight;
    }

    public struct BufferCopy
    {
        public uint SourceOffset;
        public uint DestinationOffset;
        public uint Size;
    }

    public struct IndirectDrawCommand
    {
        public uint VertexCount;
        public uint InstanceCount;
        public uint FirstVertex;
        public uint FirstInstance;
    }

    public struct IndexedIndirectDrawCommand
    {
        public uint IndexCount;
        public uint InstanceCount;
        public uint FirstIndex;
        public uint VertexOffset;
        public uint FirstInstance;
    }

    public struct SamplerCreateInfo
    {
        public Filter MinFilter;
        public Filter MagFilter;
        public SamplerMipmapMode MipmapMode;
        public SamplerAddressMode AddressModeU;
        public SamplerAddressMode AddressModeV;
        public SamplerAddressMode AddressModeW;
        public float MipLodBias;
        public byte AnisotropyEnable;
        public float MaxAnisotropy;
        public byte CompareEnable;
        public float MinLod;
        public float MaxLod;
        public BorderColor BorderColor;
    }

    public struct VertexBinding
    {
        public uint Binding;
        public uint Stride;
        public VertexInputRate InputRate;
        public uint StepRate;
    }

    public struct VertexAttribute
    {
        public uint Location;
        public uint Binding;
        public VertexElementFormat Format;
        public uint Offset;
    }

    public struct VertexInputState
    {
        public VertexBinding* vertexBindings;
        public uint VertexBindingCount;
        public VertexAttribute* vertexAttributes;
        public uint VertexAttributeCount;
    }

    public struct StencilOpState
    {
        public StencilOp FailOp;
        public StencilOp PassOp;
        public StencilOp DepthFailOp;
        public StencilOp CompareOp;
    }

    public struct ColorAttachmentBlendState
    {
        public byte BlendEnable;
        public BlendFactor SourceColorBlendFactor;
        public BlendFactor DestinationColorBlendFactor;
        public BlendOp ColorBlendOp;
        public BlendFactor SourceAlphaBlendFactor;
        public BlendFactor DestinationAlphaBlendFactor;
        public BlendOp AlphaBlendOp;
        public ColorComponentFlags ColorWriteMask;
    }

    public struct ShaderCreateInfo
    {
        public nuint CodeSize;
        public byte* Code;
        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string EntryPointName;
        public ShaderStage Stage;
        public ShaderFormat Format;
    }

    public struct TextureCreateInfo
    {
        public uint Width;
        public uint Height;
        public uint Depth;
        public byte IsCube;
        public uint LayerCount;
        public uint LevelCount;
        public SampleCount SampleCount;
        public TextureFormat Format;
        public TextureUsageFlags UsageFlags;
    }

    public struct RasterizerState
    {
        public FillMode FillMode;
        public CullMode CullMode;
        public FrontFace FrontFace;
        public byte DepthBiasEnable;
        public float DepthBiasConstantFactor;
        public float DepthBiasClamp;
        public float DepthBiasSlopeFactor;
    }

    public struct MultisampleState
    {
        public SampleCount MultisampleCount;
        public uint SampleMask;
    }

    public struct DepthStencilState
    {
        public byte DepthTestEnable;
        public byte DepthWriteEnable;
        public CompareOp CompareOp;
        public byte DepthBoundsTestEnable;
        public byte StencilTestEnable;
        public StencilOpState BackStencilState;
        public StencilOpState FrontStencilState;
        public uint CompareMask;
        public uint WriteMask;
        public uint Reference;
        public float MinDepthBounds;
        public float MaxDepthBounds;
    }

    public struct ColorAttachmentDescription
    {
        public TextureFormat Format;
        public ColorAttachmentBlendState BlendState;
    }

    public struct GraphicsPipelineAttachmentInfo
    {
        public ColorAttachmentDescription* ColorAttachmentDescriptions;
        public uint ColorAttachmentCount;
        public byte HasDepthStencilAttachment;
        public TextureFormat DepthStencilFormat;
    }

    public struct GraphicsPipelineResourceInfo
    {
        public uint SamplerCount;
        public uint StorageBufferCount;
        public uint StorageTextureCount;
        public uint UniformBufferCount;
    }

    public struct GraphicsPipelineCreateInfo
    {
        public nint VertexShader;
        public nint FragmentShader;
        public VertexInputState VertexInputState;
        public PrimitiveType PrimitiveType;
        public RasterizerState RasterizerState;
        public MultisampleState MultisampleState;
        public DepthStencilState DepthStencilState;
        public GraphicsPipelineAttachmentInfo AttachmentInfo;
        public GraphicsPipelineResourceInfo VertexResourceInfo;
        public GraphicsPipelineResourceInfo FragmentResourceInfo;
        public fixed float BlendConstants[4];
    }

    public struct ComputePipelineResourceInfo
    {
        public uint ReadOnlyStorageTextureCount;
        public uint ReadOnlyStorageBufferCount;
        public uint ReadWriteStorageTextureCount;
        public uint ReadWriteStorageBufferCount;
        public uint UniformBufferCount;
    }

    public struct ComputePipelineCreateInfo
    {
        public nint ComputeShader;
        public ComputePipelineResourceInfo PipelineResourceInfo;
    }

    public struct ColorAttachmentInfo
    {
        public TextureSlice TextureSlice;
        public Color ClearColor;
        public LoadOp publicLoadOp;
        public StoreOp StoreOp;
        public int Cycle; /* SDL_bool */
    }

    public struct DepthStencilAttachmentInfo
    {
        public TextureSlice TextureSlice;
        public DepthStencilValue DepthStencilClearValue;
        public LoadOp LoadOp;
        public StoreOp StoreOp;
        public LoadOp StencilLoadOp;
        public StoreOp StencilStoreOp;
        public int Cycle; /* SDL_bool */
    }

    public struct BufferBinding
    {
        public nint Buffer;
        public uint Offset;
    }

    public struct TextureSamplerBinding
    {
        public nint Texture;
        public nint Sampler;
    }

    public struct StorageBufferReadWriteBinding
    {
        public nint Buffer;
        public int Cycle; /* SDL_bool */
    }

    public struct StorageTextureReadWriteBinding
    {
        public TextureSlice TextureSlice;
        public int Cycle; /* SDL_bool */
    }

    /* Functions */

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateDevice(
        BackendFlags preferredBackends,
        int debugMode /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDestroyDevice(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern BackendFlags SDL_GpuGetBackend(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateComputePipeline(
        nint device,
        in ComputePipelineCreateInfo computePipelineCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateGraphicsPipeline(
        nint device,
        in GraphicsPipelineCreateInfo graphicsPipelineCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateSampler(
        nint device,
        in SamplerCreateInfo samplerCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateShader(
        nint device,
        in ShaderCreateInfo shaderCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateTexture(
        nint device,
        in TextureCreateInfo textureCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateBuffer(
        nint device,
        BufferUsageFlags usageFlags,
        uint sizeInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateTransferBuffer(
        nint device,
        TransferUsage usage,
        TransferBufferMapFlags mapFlags,
        uint sizeInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuCreateOcclusionQuery(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSetBufferName(
        nint device,
        nint buffer,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string text
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSetTextureName(
        nint device,
        nint texture,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string text
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseTexture(
        nint device,
        nint texture
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseSampler(
        nint device,
        nint sampler
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseBuffer(
        nint device,
        nint buffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseTransferBuffer(
        nint device,
        nint transferBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseShader(
        nint device,
        nint shader
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseGraphicsPipeline(
        nint device,
        nint graphicsPipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseComputePipeline(
        nint device,
        nint computePipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseOcclusionQuery(
        nint device,
        nint query
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuBeginRenderPass(
        nint commandBuffer,
        ColorAttachmentInfo* colorAttachmentInfos,
        uint colorAttachmentCount,
        DepthStencilAttachmentInfo* depthStencilAttachmentInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindGraphicsPipeline(
        nint renderPass,
        nint graphicsPipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSetViewport(
        nint renderPass,
        in Viewport viewport
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSetScissor(
        nint renderPass,
        in Rect scissor
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindVertexBuffers(
        nint renderPass,
        uint firstBinding,
        BufferBinding *bindings,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindIndexBuffer(
        nint renderPass,
        in BufferBinding binding,
        IndexElementSize indexElementSize
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindVertexSamplers(
        nint renderPass,
        uint firstSlot,
        TextureSamplerBinding* textureSamplerBindings,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindVertexStorageTextures(
        nint renderPass,
        uint firstSlot,
        TextureSlice *storageTextureSlices,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindVertexStorageBuffers(
        nint renderPas,
        uint firstSlot,
        nint* storageBuffers,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindFragmentSamplers(
        nint renderPass,
        uint firstSlot,
        TextureSamplerBinding* textureSamplerBindings,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindFragmentStorageTextures(
        nint renderPass,
        uint firstSlot,
        TextureSlice *storageTextureSlices,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindFragmentStorageBuffers(
        nint renderPass,
        uint firstSlot,
        nint* storageBuffers,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuPushVertexUniformData(
        nint renderPass,
        uint slotIndex,
        nint data,
        uint dataLengthInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuPushFragmentUniformData(
        nint renderPass,
        uint slotIndex,
        nint data,
        uint dataLengthInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDrawIndexedPrimitives(
        nint renderPass,
        uint baseVertex,
        uint startIndex,
        uint primitiveCount,
        uint instanceCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDrawPrimitives(
        nint renderPass,
        uint vertexStart,
        uint primitiveCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDrawPrimitivesIndirect(
        nint renderPass,
        nint buffer,
        uint offsetInBytes,
        uint drawCount,
        uint stride
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDrawIndexedPrimitivesIndirect(
        nint renderPass,
        nint buffer,
        uint offsetInBytes,
        uint drawCount,
        uint stride
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuEndRenderPass(
        nint renderPass
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuBeginComputePass(
        nint commandBuffer,
        StorageTextureReadWriteBinding* storageTextureBindings,
        uint storageTextureBindingCount,
        StorageBufferReadWriteBinding* storageBufferBindings,
        uint storageBufferBindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindComputePipeline(
        nint computePass,
        nint computePipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindComputeStorageTextures(
        nint computePass,
        uint firstSlot,
        TextureSlice* storageTextureSlices,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBindComputeStorageBuffers(
        nint computePass,
        uint firstSlot,
        nint* storageBuffers,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuPushComputeUniformData(
        nint computePass,
        uint slotIndex,
        nint dat,
        uint dataLengthInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDispatchCompute(
        nint computePass,
        uint groupCountX,
        uint groupCountY,
        uint groupCountZ
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuEndComputePass(
        nint computePass
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuMapTransferBuffer(
        nint device,
        nint transferBuffer,
        int cycle, /* SDL_bool */
        out nint ppData
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuUnmapTransferBuffer(
        nint device,
        nint transferBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSetTransferData(
        nint device,
        nint data,
        nint transferBuffer,
        in BufferCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuGetTransferData(
        nint device,
        nint transferBuffer,
        nint data,
        in BufferCopy copyParams
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuBeginCopyPass(
        nint commandBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuUploadToTexture(
        nint copyPass,
        nint transferBuffer,
        in TextureRegion textureRegion,
        in BufferImageCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuUploadToBuffer(
        nint copyPass,
        nint transferBuffer,
        nint buffer,
        in BufferCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuCopyTextureToTexture(
        nint copyPass,
        in TextureRegion source,
        in TextureRegion destination,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuCopyBufferToBuffer(
        nint copyPass,
        nint source,
        nint destination,
        in BufferCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GenerateMipmaps(
        nint copyPass,
        nint texture
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDownloadFromTexture(
        nint copyPass,
        in TextureRegion textureRegion,
        nint transferBuffer,
        in BufferImageCopy copyParams
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuDownloadFromBuffer(
        nint copyPass,
        nint buffer,
        nint transferBuffer,
        in BufferCopy copyParams
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuEndCopyPass(
        nint copyPass
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuBlit(
        nint commandBuffer,
        in TextureRegion source,
        in TextureRegion destination,
        Filter filterMode,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GpuSupportsSwapchainComposition(
        nint device,
        nint window,
        SwapchainComposition swapchainComposition
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GpuSupportsPresentMode(
        nint device,
        nint window,
        PresentMode presentMode
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GpuClaimWindow(
        nint device,
        nint window,
        SwapchainComposition swapchainComposition,
        PresentMode presentMode
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuUnclaimWindow(
        nint device,
        nint window
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSetSwapchainParameters(
        nint device,
        nint window,
        SwapchainComposition swapchainComposition,
        PresentMode presentMode
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern TextureFormat SDL_GpuGetSwapchainTextureFormat(
        nint device,
        nint window
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuAcquireCommandBuffer(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuAcquireSwapchainTexture(
        nint commandBuffer,
        nint window,
        out uint width,
        out uint height
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuSubmit(
        nint commandBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint SDL_GpuSubmitAndAcquireFence(
        nint commandBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuWait(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuWaitForFences(
        nint device,
        byte waitAll,
        uint fenceCount,
        nint* fences
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GpuQueryFence(
        nint device,
        nint fence
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuReleaseFence(
        nint device,
        nint fence
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint SDL_GpuTextureFormatTexelBlockSize(
        TextureFormat textureFormat
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GpuIsTextureFormatSupported(
        nint device,
        TextureFormat format,
        TextureType type,
        TextureUsageFlags usage
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SampleCount SDL_GpuGetBestSampleCount(
        nint device,
        TextureFormat format,
        SampleCount desiredSampleCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuOcclusionQueryBegin(
        nint commandBuffer,
        nint query
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GpuOcclusionQueryEnd(
        nint commandBuffer,
        nint query
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDL_GpuOcclusionQueryPixelCount(
        nint device,
        nint query,
        out uint pixelCount
    );
}
