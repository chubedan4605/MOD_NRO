using System;
using System.Text;

// Token: 0x0200002F RID: 47
public class myWriter
{
	// Token: 0x06000204 RID: 516 RVA: 0x000052A3 File Offset: 0x000034A3
	public myWriter()
	{
	}

	// Token: 0x06000205 RID: 517 RVA: 0x000052C6 File Offset: 0x000034C6
	public myWriter(int len)
	{
		this.buffer = new sbyte[len];
		this.lenght = len;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00013A08 File Offset: 0x00011C08
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00013A38 File Offset: 0x00011C38
	public void writeSByteUncheck(sbyte value)
	{
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x000052FC File Offset: 0x000034FC
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00005305 File Offset: 0x00003505
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000530F File Offset: 0x0000350F
	public void writeChar(char value)
	{
		this.writeSByte(0);
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00005305 File Offset: 0x00003505
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00013A60 File Offset: 0x00011C60
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00013A94 File Offset: 0x00011C94
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00013AC8 File Offset: 0x00011CC8
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00013B00 File Offset: 0x00011D00
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00013AC8 File Offset: 0x00011CC8
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00013B38 File Offset: 0x00011D38
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00013B70 File Offset: 0x00011D70
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00005320 File Offset: 0x00003520
	public void writeBoolean(bool value)
	{
		this.writeSByte((sbyte)((!value) ? 0 : 1));
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00005320 File Offset: 0x00003520
	public void writeBool(bool value)
	{
		this.writeSByte((sbyte)((!value) ? 0 : 1));
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00013BA8 File Offset: 0x00011DA8
	public void writeString(string value)
	{
		char[] array = value.ToCharArray();
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00013BF0 File Offset: 0x00011DF0
	public void writeUTF(string value)
	{
		Encoding unicode = Encoding.Unicode;
		Encoding encoding = Encoding.GetEncoding(65001);
		byte[] bytes = unicode.GetBytes(value);
		byte[] array = Encoding.Convert(unicode, encoding, bytes);
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		foreach (sbyte value2 in array)
		{
			this.writeSByteUncheck(value2);
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00013C5C File Offset: 0x00011E5C
	public void write(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			this.writeSByte(data[i + arg1]);
			if (this.posWrite > this.buffer.Length)
			{
				return;
			}
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00005335 File Offset: 0x00003535
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00013CA4 File Offset: 0x00011EA4
	public sbyte[] getData()
	{
		if (this.posWrite <= 0)
		{
			return null;
		}
		sbyte[] array = new sbyte[this.posWrite];
		for (int i = 0; i < this.posWrite; i++)
		{
			array[i] = this.buffer[i];
		}
		return array;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00013CF0 File Offset: 0x00011EF0
	public void checkLenght(int ltemp)
	{
		if (this.posWrite + ltemp > this.lenght)
		{
			sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
			for (int i = 0; i < this.lenght; i++)
			{
				array[i] = this.buffer[i];
			}
			this.buffer = null;
			this.buffer = array;
			this.lenght += 1024 + ltemp;
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000533E File Offset: 0x0000353E
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000533E File Offset: 0x0000353E
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x040001ED RID: 493
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x040001EE RID: 494
	private int posWrite;

	// Token: 0x040001EF RID: 495
	private int lenght = 2048;
}
