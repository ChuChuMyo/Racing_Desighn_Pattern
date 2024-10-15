using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Chapter.SpatialPartition
{
    public class TrackController : MonoBehaviour
    {
        private float _trackSpeed;
        private Transform _prevSeg;
        private GameObject _trackParent;
        private Transform _segParent;
        private List<GameObject> _segments;
        private Stack<GameObject> _segStack;

        private Vector3 _currentPosition = new Vector3(0, 0, 0);

        [Tooltip("List of race tracks")]
        [SerializeField]
        private Track track;

        [Tooltip("Initial amount of segment to load at start")]
        [SerializeField]
        private int initSegAmount;

        [Tooltip("Dampen the speed of the the track")]
        [Range(0.0f, 100.0f)]
        [SerializeField]
        private float speedDampener;

        private void Awake()
        {
            //System.Linq�� �����ϴ� Reverse() �޼���� Enumerable Ŭ�������� �����Ǹ� IEnumerable<TSource> Ÿ���� ��ȯ
            _segments = Enumerable.Reverse(track.segments).ToList(); //ToList(): Linq ���(�÷���)
        }

        private void Start()
        {
            InitTrack();
        }

        // Update is called once per frame
        void Update()
        {
            _segParent.transform.Translate(Vector3.back * (_trackSpeed * Time.deltaTime));
        }

        //Ʈ�� ���׸�Ʈ�� �����̳� ������ �� Ʈ�� ���� ������Ʈ�� �ν��Ͻ�ȭ �Ѵ�.
        private void InitTrack()
        {
            Destroy(_trackParent);

            _trackParent = Instantiate(Resources.Load("Track", typeof(GameObject))) as GameObject; //as ������ : �� ��ȯ

            if (_trackParent)
                _segParent = _trackParent.transform.Find("Segments");

            _prevSeg = null;

            //���׸�Ʈ ����� ���ο� Stack �����̳ʿ� �ִ´�.
            _segStack = new Stack<GameObject>(_segments);

            LoadSegment(initSegAmount);
        }

        private void LoadSegment(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if(_segStack.Count > 0)
                {
                    GameObject segment = Instantiate(_segStack.Pop(), _segParent.transform);

                    if (!_prevSeg)
                        _currentPosition.z = 0;

                    if (_prevSeg)
                        _currentPosition.z = _prevSeg.position.z + track.segmentLength;

                    segment.transform.position = _currentPosition;

                    segment.AddComponent<Segment>();

                    segment.GetComponent<Segment>().trackController = this;

                    _prevSeg = segment.transform;
                }
            }
        }

        public void LoadNextSegment()
        {
            LoadSegment(initSegAmount);
        }
    }

}