#pragma once

///////////////////////////////////////////////////
//  FDList.h       :  V0001
//  一个快速的双向链表操作类
//  Write   By       :  DreamRoom(赖世凯)
//  CopyRight(By)    :  DreamRoom
//
//  V0001            :  2009/07/04
//  V0002            :  2012/07/11
////////////////////////////////////////////////////

#ifndef ASSERT
#define ASSERT(exp)
#endif

template <class _Ty>
struct   SFDListNode
{
	_Ty   *m_pLast;
	_Ty   *m_pNext;
	SFDListNode():m_pLast(NULL), m_pNext(NULL){}
};

#ifndef  DEBUG_INIT_VALUE
#ifdef   _DEBUG
#define  DEBUG_DEFINE_VALUE(type, name)        type    name;
#define  DEBUG_INIT_VALUE(name, value)      , name(value)
#define  DEBUG_SET_VALUE(name, value)          name = value
#define  DEBUG_IS_SAME(name1, name2)           name1 == name2
#define  DEBUG_INPUT_VALUE(node, list)         node, list
#define  DEBUG_INPUT_DECLARE(_Ty, node, _TyList, list)   const _Ty *node, const _TyList * list
#else
#define  DEBUG_DEFINE_VALUE(type, name)
#define  DEBUG_INIT_VALUE(name, value)
#define  DEBUG_SET_VALUE(name, value)
#define  DEBUG_IS_SAME(name1, name2)           true
#define  DEBUG_INPUT_VALUE(node, list)         node
#define  DEBUG_INPUT_DECLARE(_Ty, node, _TyList, list)   const _Ty *node
#endif
#endif


	template <class _TyNode, class _TyList>
	class const_iterator_fast_list
	{
		friend _TyList;
	protected:
		const _TyNode				*m_pNode;
		DEBUG_DEFINE_VALUE(const _TyList*, m_pList)
		const_iterator_fast_list( DEBUG_INPUT_DECLARE(_TyNode, pNode, _TyList, pList) ):m_pNode(pNode)
			DEBUG_INIT_VALUE(m_pList, pList)
		{
		}
	public:
		const_iterator_fast_list():m_pNode(NULL){}
		const_iterator_fast_list( const const_iterator_fast_list &p ):m_pNode(p.m_pNode)
			DEBUG_INIT_VALUE(m_pList, NULL)
		{
		}
		const const_iterator_fast_list &operator = ( const const_iterator_fast_list &p){
			m_pNode = p.m_pNode;
			DEBUG_SET_VALUE(m_pList, p.m_pList);
			return *this;
		}
		const_iterator_fast_list  operator ++(int){
			_TyNode  *pNode = m_pNode;
			++m_pNode;
			return  const_iterator_fast_list(pNode);
		}
		const_iterator_fast_list operator --(int){
			_TyNode  *pNode = m_pNode;
			--m_pNode;
			return  const_iterator_fast_list(pNode);
		}
		int operator ++(void) {
			m_pNode = m_pNode->m_pNext;
			return 1;
		}
		int operator --(void) {
			m_pNode = m_pNode->m_pLast;
			return 1;
		}
		bool   IsValidList( const _TyList  *pList ) const
		{
			return  DEBUG_IS_SAME(m_pList, pList);
		}
		const _TyNode  *get_ptr() const
		{
			return  m_pNode;
		}
	private:
		operator bool(void) const;
		bool operator !(void) const;
	public:
		bool  operator == ( const const_iterator_fast_list &p ){
			return  m_pNode == p.m_pNode;
		}
		bool  operator != ( const const_iterator_fast_list &p ){
			return  m_pNode != p.m_pNode;
		}
		const _TyNode &operator *(void) const{
			return *m_pNode;
		}
		operator const _TyNode*(void) const{
			return m_pNode;
		}
		const _TyNode *operator ->(void) const{
			return m_pNode;
		}
	};
	template <class _TyNode, class _TyList>
	class iterator_fast_list : public const_iterator_fast_list<_TyNode, _TyList>
	{
		typedef const_iterator_fast_list<_TyNode, _TyList>   base_type;
		friend _TyList;
	protected:
		//iterator( const LIST_NODE *pNode, const CFastList<LIST_NODE> *pList ) : const_iterator(pNode, pList){}
		iterator_fast_list( DEBUG_INPUT_DECLARE(_TyNode, pNode, _TyList, pList) ) : base_type( DEBUG_INPUT_VALUE(pNode, pList) ){}
	public:
		iterator_fast_list(){}
		iterator_fast_list( const iterator_fast_list &p ):base_type(p){}
		iterator_fast_list( const base_type &p ):base_type(p){}
		const iterator_fast_list &operator = ( const base_type &p){
			return  *((base_type*)this) = p;
		}
		const iterator_fast_list &operator = ( const iterator_fast_list &p){
			*((base_type*)this) = p;
			return *this;
		}
		operator _TyNode*(void){ return (_TyNode*)base_type::m_pNode; }
		_TyNode &operator *(void){
			return *((_TyNode*)base_type::m_pNode);
		}
		_TyNode *operator ->(void){ return (_TyNode*)base_type::m_pNode; }
		_TyNode  *get_ptr()
		{
			return  (_TyNode*)base_type::m_pNode;
		}
	};


template <class LIST_NODE>
class  CFastList
{
public:
	typedef CFastList<LIST_NODE>    this_type;
	typedef const_iterator_fast_list<LIST_NODE, this_type>  const_iterator;
	typedef iterator_fast_list<LIST_NODE, this_type>  iterator;
public:
	CFastList():m_pBegin(NULL), m_pEnd(NULL), m_nCount(0){}
	CFastList( const CFastList & p ):m_pBegin(NULL), m_pEnd(NULL), m_nCount(0){ ASSERT(p.m_nCount == 0); }
	const CFastList &operator = ( const CFastList & p )		
	{ 
		ASSERT(p.m_nCount == 0); 
		return *this;
	}
public:
	int    GetCount() const{
		return  m_nCount;
	}
public:
	bool   empty() const{ 
		return  m_nCount == 0 ;
	}
public:
	void            clear()
	{
		m_pBegin = m_pEnd = NULL;
		m_nCount = 0;
	}
	LIST_NODE  *front_ptr(){
		return m_pBegin;
	}
	const LIST_NODE  *front_ptr() const {
		return m_pBegin;
	}
	LIST_NODE       &front(){ return *m_pBegin; }
	const LIST_NODE &front() const { return *m_pBegin; }
	LIST_NODE       &back(){ return  *m_pEnd;	}
	const LIST_NODE &back() const{ return *m_pEnd; }
	int             size() const{ return m_nCount; }
	iterator        begin(){ return  iterator( DEBUG_INPUT_VALUE(m_pBegin, this)); }
	iterator        end(){ return  iterator( DEBUG_INPUT_VALUE(NULL, this) ); }
	const_iterator  begin() const{ return  const_iterator( DEBUG_INPUT_VALUE(m_pBegin, this) ); }
	const_iterator  end() const{ return  const_iterator( DEBUG_INPUT_VALUE(NULL, this) ); }
	void            push_back( LIST_NODE *pNode )
	{
		if( m_pEnd )
		{
			m_pEnd->m_pNext = pNode;
			pNode->m_pLast  = m_pEnd;
			m_pEnd = pNode;
			m_pEnd->m_pNext = NULL;
		}
		else
		{
			m_pBegin = m_pEnd = pNode;
			pNode->m_pNext = pNode->m_pLast = NULL;
		}
		++m_nCount;
	}
	void            push_front( LIST_NODE *pNode )
	{
		if( m_pBegin )
		{
			ASSERT(!pNode->m_pLast);
			m_pBegin->m_pLast = pNode;
			pNode->m_pNext    = m_pBegin;
			m_pBegin = pNode;
			m_pBegin->m_pLast = NULL;
		}
		else
		{
			m_pBegin = m_pEnd = pNode;
			pNode->m_pNext = pNode->m_pLast = NULL;
		}
		++m_nCount;
	}

	// 功能：弹出最后一个节点
	void            pop_back()
	{
		if( m_pEnd )
		{
			LIST_NODE   *pNode = m_pEnd;
			m_pEnd = m_pEnd->m_pLast;
			--m_nCount;
			if( m_pEnd )
				m_pEnd->m_pNext = NULL;
			else
			{
				ASSERT( m_nCount == 0 );
				m_pBegin = NULL;
			}

			pNode->m_pLast = NULL;
			pNode->m_pNext = NULL;
		}
	}
	void            pop_front()
	{
		if( m_pBegin )
		{
			LIST_NODE   *pNode = m_pBegin;
			m_pBegin = m_pBegin->m_pNext;
			--m_nCount;
			if( m_pBegin )
				m_pBegin->m_pLast = NULL;
			else
			{
				ASSERT( m_nCount == 0 );
				m_pEnd = NULL;
			}
			pNode->m_pLast = NULL;
			pNode->m_pNext = NULL;
		}
	}
	iterator        insert(const iterator &_Where, LIST_NODE *pNode)
	{
		ASSERT( _Where.IsValidList(this) );  // 测试是否是合法的节点
		LIST_NODE  *pInsertWhere   = (LIST_NODE*)_Where.m_pNode;
		if( NULL == pInsertWhere )
		{
			push_back(pNode);
			return  end();
		}
		else
		{
			ASSERT( m_nCount > 0 );
			LIST_NODE  *pLast = pInsertWhere->m_pLast;
			if( pLast )
				pLast->m_pNext = pNode;
			pNode->m_pNext = pInsertWhere;
			pNode->m_pLast = pLast;
			pInsertWhere->m_pLast = pNode;
			if( pInsertWhere == m_pBegin )
				m_pBegin = pNode;
			++m_nCount;
			return  _Where;
		}
	}
	iterator        insert(const iterator &_Where, LIST_NODE *pBegin, LIST_NODE *pEnd, int nCount)
	{
		if( nCount < 0 )
			return  _Where;
		if( nCount <= 1 )
		{
			push_back(pBegin);
			return  end();
		}

		ASSERT( _Where.IsValidList(this) );  // 测试是否是合法的节点
		LIST_NODE  *pInsertWhere   = (LIST_NODE*)_Where.m_pNode;
		if( NULL == pInsertWhere )
		{
			m_pBegin = pBegin;
			m_pEnd   = pEnd;
			m_nCount = nCount;
		}
		else
		{
			ASSERT( m_nCount > 0 );

			pEnd->m_pNext = m_pBegin;
			m_pBegin->m_pLast = pEnd;
			m_pBegin = pBegin;
			m_nCount += nCount;
		}
		return  _Where;
	}
	iterator        erase(const iterator &_Where)
	{
		ASSERT( _Where.IsValidList(this) );  // 测试是否是合法的节点

		LIST_NODE  *pKey   = (LIST_NODE*)_Where.m_pNode;

		LIST_NODE  *pNext = pKey->m_pNext;

		if( pKey->m_pLast )
			pKey->m_pLast->m_pNext = pKey->m_pNext;
		if( pKey->m_pNext )
			pKey->m_pNext->m_pLast = pKey->m_pLast;

		if( pKey == m_pBegin )
			m_pBegin = pKey->m_pNext;
		if( pKey == m_pEnd )
			m_pEnd = m_pEnd->m_pLast;
		--m_nCount;

		pKey->m_pLast = NULL;
		pKey->m_pNext = NULL;

		return  iterator( DEBUG_INPUT_VALUE(pNext, this) );
	}
	void  swap( CFastList & p )
	{
		LIST_NODE  *pTemp = m_pBegin; m_pBegin = p.m_pBegin; p.m_pBegin = pTemp;
		pTemp = m_pEnd; m_pEnd = p.m_pEnd; p.m_pEnd = pTemp;
		int  nTemp = m_nCount; m_nCount = p.m_nCount; p.m_nCount = nTemp;
	}
	void  splice( const iterator &_Where, CFastList & p )
	{
		ASSERT( _Where.IsValidList(this) );  // 测试是否是合法的节点
		if( p.m_nCount <= 0 )
			return ;
		LIST_NODE  *pNode = (LIST_NODE*)_Where.m_pNode;
		if( NULL == pNode )
		{
			splice(p);
		}
		else if( pNode == m_pBegin )
		{
			p.m_pEnd->m_pNext = m_pBegin;
			m_pBegin->m_pLast = p.m_pEnd;

			m_pBegin = p.m_pBegin;
			m_pEnd   = p.m_pEnd;

			m_nCount += p.m_nCount;
		}
		else
		{
			LIST_NODE  *pLast = pNode->m_pLast;
			p.m_pEnd->m_pNext = pNode;
			pNode->m_pLast = p.m_pEnd;
            pLast->m_pNext = p.m_pBegin;
			p.m_pBegin->m_pLast = pLast;

			m_nCount += p.m_nCount;
		}
		p.m_pBegin = p.m_pEnd = NULL;
		p.m_nCount = 0;
	}
	void  splice( CFastList & p )
	{
		if( p.m_nCount <= 0 )
			return ;
		if( m_nCount <= 0 )
		{
			m_pBegin = p.m_pBegin; m_pEnd = p.m_pEnd; m_nCount = p.m_nCount;
		}
		else
		{
			m_pEnd->m_pNext = p.m_pBegin;
			p.m_pBegin->m_pLast = m_pEnd;
			m_pEnd = p.m_pEnd;
			m_nCount += p.m_nCount;
		}
		p.m_pBegin = p.m_pEnd = NULL;
		p.m_nCount = 0;
	}
public:
	// 测试是否是合法的节点
	//bool   IsValidIterator( const iterator &_Where ) const{
	//	return  _Where.IsValidList(this);
	//}
	iterator   MakeIterator( LIST_NODE *pNode )
	{
		return  iterator( DEBUG_INPUT_VALUE(pNode, this) );
	}
protected:
	LIST_NODE    *m_pBegin;
	LIST_NODE    *m_pEnd;
	int           m_nCount;
};