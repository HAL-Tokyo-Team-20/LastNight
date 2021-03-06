
// 敌人FSM状态机枚举
public enum EnemyFSMState
{
    //待机
    Idle,
    //发现玩家
    SeePlayer,
    //攻击玩家
    AttackPlayer,
    //死亡
    Dead,
}

// SkyCar的移动方向枚举
public enum MoveDirEnum
{
    Left,
    Right,
    Forward,
}

// 敌人类型枚举
public enum EnemyType
{

}

// 義肢类型枚举
public enum ProstheticType
{
    Gun, //手枪, 默认
    ShotGun, //霰弹枪
    MiniGun, //加特林
}

// UIType类型枚举
public enum UI_ObjectEnum
{
    Text_Hint,
    BlackFrame,
    Text_Info,
    Image_Frame,
    Image_SelectItem,
    Text_Debug,
    END,
}