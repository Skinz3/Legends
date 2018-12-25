using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class CreateNeutralMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_CreateNeutral;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public NetNodeEnum netNodeId;
        public Vector3 position;
        public Vector3 groupPosition;
        public Vector3 faceDirectionPosition;
        public string name;
        public string skinName;
        public string uniqueName;
        public string spawnAnimationName;
        public TeamId teamId;
        public int damageBonus;
        public int healthBonus;
        public MinionRoamState roamState;
        public int groupNumber;
        public int buffSide;
        public int revealEvent;
        public int initialLevel;
        public float spawnDuration;
        public float spawnTime;
        public byte BehaviorTree;
        public string AIScript;


        public CreateNeutralMessage()
        {

        }
        public CreateNeutralMessage(uint netId, NetNodeEnum netNode, Vector3 position, Vector3 groupPosition, Vector3 faceDirectionPosition,
            string name, string skinName, string uniqueName, string spawnAnimationName, TeamId teamId, int damageBonus,
            int healthBonus, MinionRoamState roamState, int groupNumber, int buffSide, int revealEvent,
            int initialLevel, float spawnDuration, float spawnTime, byte behaviorTree, string aiScript) : base(netId)
        {
            this.netNodeId = netNode;
            this.position = position;
            this.groupPosition = groupPosition;
            this.faceDirectionPosition = faceDirectionPosition;
            this.name = name;
            this.skinName = skinName;
            this.uniqueName = uniqueName;
            this.spawnAnimationName = spawnAnimationName;
            this.teamId = teamId;
            this.damageBonus = damageBonus;
            this.healthBonus = healthBonus;
            this.roamState = roamState;
            this.groupNumber = groupNumber;
            this.buffSide = buffSide;
            this.revealEvent = revealEvent;
            this.initialLevel = initialLevel;
            this.spawnDuration = spawnDuration;
            this.spawnTime = spawnTime;
            this.BehaviorTree = behaviorTree;
            this.AIScript = aiScript;
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(netId);
            writer.WriteByte((byte)netNodeId);
            position.Serialize(writer);
            groupPosition.Serialize(writer);
            faceDirectionPosition.Serialize(writer);
            writer.WriteString(name, 64);
            writer.WriteString(skinName, 64);
            writer.WriteString(uniqueName, 64);
            writer.WriteString(spawnAnimationName, 64);
            writer.WriteUInt((uint)teamId);
            writer.WriteInt(damageBonus);
            writer.WriteInt(healthBonus);
            writer.WriteUInt((uint)roamState);
            writer.WriteInt(groupNumber);
            writer.WriteInt(buffSide);
            writer.WriteInt(revealEvent);
            writer.WriteInt(initialLevel);
            writer.WriteFloat(spawnDuration);
            writer.WriteFloat(spawnTime);
            writer.WriteByte(BehaviorTree);
            writer.WriteFixedStringLast(AIScript, 32);

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
