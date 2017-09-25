namespace LGI.Core.Model
{
    /// <summary>
    /// PIS���ýӿڴ���Ĳ�ѯ����
    /// </summary>
    public class GetHisInfo
    {
        private string _sHISName;
        private string _Sslbx;
        private string _Ssbz;
        private bool _Debug;
        private string _by;

        /// <summary>
        /// ҽԺ����,��LGI.HSP������ҽԺ�ӿ�ʵ�����ƶ�Ӧ,��ʹ��������<
        /// </summary>
        public string SHisName
        {
            get { return _sHISName; }
            set { _sHISName = value; }
        }

        /// <summary>
        /// ��ѯ����,�������,סԺ�ŵ�,PIS�˿ɶ�̬ά��
        /// </summary>
        public string Sslbx
        {
            get { return _Sslbx; }
            set { _Sslbx = value; }
        }

        /// <summary>
        /// ��ѯ����,�������,סԺ�ŵľ���ֵ
        /// </summary>
        public string Ssbz
        {
            get { return _Ssbz; }
            set { _Ssbz = value; }
        }

        /// <summary>
        /// �Ƿ����ģʽ,����ģʽ��log���¼������Ϣ
        /// </summary>
        public bool Debug
        {
            get { return _Debug; }
            set { _Debug = value; }
        }

        public string By
        {
            get { return _by; }
            set { _by = value; }
        }
    }
}