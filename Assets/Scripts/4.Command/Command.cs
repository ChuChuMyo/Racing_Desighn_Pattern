using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Command
{
    //�߻� Ŭ����
    public abstract class Command
    {
        //�߻� �޼��� �ڽ� Ŭ�������� override Ű���带 ����Ͽ� ������ �Ͽ� ����
        public abstract void Execute();
    }
}
