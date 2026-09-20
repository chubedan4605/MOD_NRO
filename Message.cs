using System;

// Token: 0x02000024 RID: 36
public class Message
{
	// Token: 0x0600014D RID: 333 RVA: 0x00004CDD File Offset: 0x00002EDD
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00004CF8 File Offset: 0x00002EF8
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00004D0B File Offset: 0x00002F0B
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00004D25 File Offset: 0x00002F25
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00004D40 File Offset: 0x00002F40
	public sbyte[] getData()
	{
		if (this.dos != null)
		{
			return this.dos.getData();
		}
		if (this.dis != null)
		{
			return this.dis.buffer;
		}
		return null;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00004D4D File Offset: 0x00002F4D
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00004D55 File Offset: 0x00002F55
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00004D5D File Offset: 0x00002F5D
	public int readInt3Byte()
	{
		return this.dis.readInt();
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000045ED File Offset: 0x000027ED
	public void cleanup()
	{
	}

	// Token: 0x04000125 RID: 293
	public sbyte command;

	// Token: 0x04000126 RID: 294
	private myReader dis;

	// Token: 0x04000127 RID: 295
	private myWriter dos;
}
