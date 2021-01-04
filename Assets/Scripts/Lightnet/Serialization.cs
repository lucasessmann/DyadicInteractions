﻿using System;
using UnityEngine;

public static class SerializationHelper
{
    public static float FromBytes(byte[] data, ref int offset)
    {
        Debug.Assert(offset + sizeof(float) <= data.Length);
        float value = BitConverter.ToSingle(data, offset);
        offset += sizeof(float);
        return value;
    }

    public static void FromBytes(byte[] data, ref int offset, ref Vector3 vector)
    {
        vector.x = FromBytes(data, ref offset);
        vector.y = FromBytes(data, ref offset);
        vector.z = FromBytes(data, ref offset);
    }

    public static void FromBytes(byte[] data, ref int offset, ref Quaternion quat)
    {
        quat.x = FromBytes(data, ref offset);
        quat.y = FromBytes(data, ref offset);
        quat.z = FromBytes(data, ref offset);
        quat.w = FromBytes(data, ref offset);
    }

    public static void ToBytes(ulong value, byte[] data, ref int offset)
    {
        Debug.Assert(offset + sizeof(float) < data.Length);
        byte[] buffer = BitConverter.GetBytes(value);
        Array.Copy(buffer, 0, data, offset, buffer.Length);
        offset += buffer.Length;
    }

    public static void ToBytes(float value, byte[] data, ref int offset)
    {
        Debug.Assert(offset + sizeof(float) <= data.Length);
        byte[] buffer = BitConverter.GetBytes(value);
        Array.Copy(buffer, 0, data, offset, buffer.Length);
        offset += buffer.Length;
    }

    public static void ToBytes(ref Vector3 vector, byte[] data, ref int offset)
    {
        ToBytes(vector.x, data, ref offset);
        ToBytes(vector.y, data, ref offset);
        ToBytes(vector.z, data, ref offset);
    }

    public static void ToBytes(ref Quaternion quat, byte[] data, ref int offset)
    {
        ToBytes(quat.x, data, ref offset);
        ToBytes(quat.y, data, ref offset);
        ToBytes(quat.z, data, ref offset);
        ToBytes(quat.w, data, ref offset);
    }
}

// This needs to be adjusted / extended to your needs
public enum ENetDataType : byte
{
    UserState = 1,
    ExperimentState = 2
}


public interface NetworkData
{
    byte[] Serialize();
    void Deserialize(byte[] data);
}

public enum EExperimentStatus
{
    Waiting,  // Waiting for other participant
    WarmUp,   // Potential Countdown until something happens
    Running,  // Information is send and received , experiment runs
}

public class ExperimentState : NetworkData
{
    private const int SIZE =
        sizeof(byte) +  // ENetDataType.ExperimentState
        sizeof(byte) +  // socket Number 
        sizeof(byte);   // ExperimentState
    public byte SocketNumber;

    public EExperimentStatus Status;
    byte[] Cache = new byte[SIZE];
        
    public byte[] Serialize()
    {
        int offset = 0;
        Cache[offset] = (byte)ENetDataType.ExperimentState;     offset += sizeof(byte);
        Cache[offset] = SocketNumber;                           offset += sizeof(byte);
        Cache[offset] = (byte) Status;
        return Cache;
    }

    public void Deserialize(byte[] data)
    {
        int offset = 0;
        Debug.Assert(data[offset]== (byte) ENetDataType.ExperimentState);   offset += sizeof(byte);
        SocketNumber = data[offset];                                        offset += sizeof(byte);
        Status = (EExperimentStatus) data[offset];
    }
}
public class UserState : NetworkData
{
    const int SIZE =
        // NOTE: using e.g. sizeof(Vector3) is not allowed...
        // so we need to always use sizeof(float) instead

        sizeof(byte) +          // header     (ENetDataType.UserState)
        sizeof(float) * 3;     // Vector3    GazeSpherePosition;


    public Vector3      GazeSpherePosition;
 
    byte[] Cache = new byte[SIZE];


    public void Deserialize(byte[] data)
    {
        int head = 0;
        Debug.Assert(data.Length >= SIZE);
        Debug.Assert((ENetDataType)data[head] == ENetDataType.UserState);
        head += sizeof(byte);

        SerializationHelper.FromBytes(data, ref head, ref GazeSpherePosition);
    }

    public byte[] Serialize()
    {
        int head = 0;
        Cache[head] = (byte)ENetDataType.UserState; head += sizeof(byte);

        SerializationHelper.ToBytes(ref GazeSpherePosition,    Cache, ref head);

        return Cache;
    }
}